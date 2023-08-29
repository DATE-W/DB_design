<!-- 我的动态 v1.2 -->
<template>
    <div class="overflow-container" ref="scrollContainer">
        <!-- 上方展示背景图 -->
        <el-container class="main-container">
            <el-main class="user-profile">
                <div class="bg-theme" :style="{ backgroundImage: 'url(' + backgroundImage + ')' }">
                </div>
                <!-- 遍历动态分组数组，为每个分组创建区域 -->
                <div class="dynamic-group" v-for="(group, groupIndex) in groupedDynamics" :key="groupIndex">
                    <!-- 遍历分组内的动态，为每个动态创建区域 -->
                    <div class="dynamic-item" v-for="(dynamic, index) in group" :key="index">
                        <!-- 显示动态的具体内容 -->
                        <div class="dynamic-content">
                            <!-- 添加 contain 元素 -->
                            <!-- 第一行：用户行为 -->
                            <div class="action-text">{{ getActionText(dynamic) }}</div>
                            <div @click="ClickToDetail(dynamic)"
                                :class="{ 'title-contain-author': dynamic.type !== 'follow', 'user-section': dynamic.type === 'follow' }">
                                <!-- 如果是用户则不用title-contain-author的样式 -->
                                <!-- 第二行：头像+用户名 或 帖子名 -->
                                <div class="avatar-and-username">
                                    <el-avatar v-if="dynamic.type === 'follow'" :src="getUserAvatar(dynamic)"
                                        class="user-avatar"></el-avatar>
                                    <div class="name-text">{{ getNameText(dynamic) }}</div>
                                </div>
                                <!-- 第三行：contain（如果存在） -->
                                <div v-if="dynamic.object.contain" class="contain-text">{{ dynamic.object.contain }}</div>
                                <!-- 第四行：帖子作者（如果存在） -->
                                <div class="avatar-and-author">
                                    <el-avatar v-if="dynamic.object.author" :src="getAuthorAvatar(dynamic)"
                                        class="author-avatar"></el-avatar>
                                    <div v-if="dynamic.object.author" class="author-text">{{ dynamic.object.author }}</div>
                                </div>
                            </div>
                            <!-- 显示动态的时间 -->
                            <div class="dynamic-time">
                                {{ FormatDate(dynamic.time) }}
                            </div>
                        </div>
                    </div>
                </div>
            </el-main>
            <el-aside class="right-profile">
                <div @click="scrollToTop" class="scroll-to-top-btn">
                    <div class="image-container">
                        <img src="../assets/img/ToTop.png" alt="Scroll to Top">
                    </div>
                    <div class="text-overlay">
                        <div class="line">{{ line1 }}</div>
                        <div class="line">{{ line2 }}</div>
                    </div>
                </div>
            </el-aside>
        </el-container>
        <!-- 展示我的动态 -->
    </div>
</template>

<script>
import { ElMessage, ElMessageBox } from 'element-plus';
import axios from 'axios';
export default {
    props: {
        backgroundImage: String, // 声明一个名为 backgroundImage 的 String 类型的 props
    },
    data() {
        return {
            line1: '回到',
            line2: '顶部',
            dynamics: [
            ],
            //backgroundImage: '',
        };
    },
    computed: {
        // 将动态按时间分组的计算属性
        groupedDynamics() {
            const groups = {};
            this.dynamics.forEach(dynamic => {
                const date = dynamic.time.split('T')[0]; // 获取日期部分，格式：YYYY-MM-DD
                if (!groups[date]) {
                    groups[date] = [];
                }
                groups[date].push(dynamic);
            });
            return Object.values(groups);
        },
    },
    mounted() {
        this.ShowAction();
        //this.getTheme();
        console.log(this.backgroundImage)
    },
    methods: {
        scrollToTop() {
            // 获取元素的引用
            const container = this.$refs.scrollContainer;
            // 使用元素的滚动方法来滚动到顶部
            if (container) {
                container.scrollTo({
                    top: 0,
                    behavior: "smooth"
                });
            }
        },
        // 根据动态类型返回相应的文本内容
        getActionText(dynamic) {
            switch (dynamic.type) {
                case 'like':
                    return `您赞同了帖子`;
                case 'follow':
                    return `您关注了用户`;
                case 'comment':
                    return `您评论了帖子`;
                case 'collect':
                    return `您收藏了帖子`;
                case 'publish':
                    return '您发布了帖子';
                default:
                    return '未知操作';
            }
        },
        // 获取用户名或帖子名
        getNameText(dynamic) {
            return dynamic.object.username || dynamic.object.title;
        },
        getUserAvatar(dynamic) {
            if (dynamic.type === 'follow') {
                // 根据需要设置用户头像的 URL
                return './src/assets/img/carousel1.png';
            }
            return null;
        },
        getAuthorAvatar(dynamic) {
            if (dynamic.object.author) {
                // 根据需要设置帖子作者头像的 URL
                return './src/assets/img/carousel1.png';
            }
            return null;
        },
        FormatDate(datetime) {
            const date = new Date(datetime);
            const year = date.getFullYear();
            const month = String(date.getMonth() + 1).padStart(2, '0');
            const day = String(date.getDate()).padStart(2, '0');
            return `${year}-${month}-${day}`;
        },
        ClickToDetail(dynamic) {
            if (dynamic.type !== 'follow') {
                this.goToDetail(dynamic.object.post_id);
            }
        },
        goToDetail(postId) {
            console.log(postId);
            this.$router.push({
                path: '/detail',
                query: { clickedPostID: postId }
            });
        },
        async ShowAction() {
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                    Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/User/userAction', {}, { headers });
                console.log(response);
                this.dynamics = response.data.actions.map(action => {
                    return {
                        type: action.type,
                        time: action.datetime,
                        object: {
                            title: action.title,
                            contain: action.contains,
                            username: action.name,
                            author: action.author,
                            post_id: action.post_id,
                        }
                    };
                });
            } catch (err) {
                ElMessage({
                    message: '失败',
                    grouping: false,
                    type: 'error',
                });
            }
        },
        async getTheme() {
            const token = localStorage.getItem('token');
            if (token == null) {
                this.isAccount = false;
                console.log(this.isAccount);
                return;
            }
            let response
            try {
                const headers = {
                    Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/User/gettheme', {}, { headers })
            } catch (err) {
                console.log(err);
                ElMessage({
                    message: '未知错误',
                    grouping: false,
                    type: 'error',
                })
                return
            }
            console.log(response);
            this.backgroundImage = response.data.image1;
            console.log(this.backgroundImage)
        }
    }
};
</script>

<style scoped>
.overflow-container {
    overflow-y: auto;
    max-height: 625px;
}

.overflow-container::-webkit-scrollbar {
    width: 0;
}

.bg-theme {
    position: relative;
    width: 100%;
    height: 250px;
    background-repeat: no-repeat;
    background-size: cover;
}

.right-profile {
    width: 15vw;
    background: linear-gradient(to top, rgb(18, 188, 240), rgb(169, 209, 244));
}

.dynamic-group {
    margin-bottom: 40px;
    /* 调整分组之间的间距,每一组为每天的动态 */
}

.dynamic-item {
    border: 1px solid #ccc;
    margin-bottom: 10px;
    padding: 10px;
}

.dynamic-content {
    font-size: 16px;
    margin-bottom: 5px;
}

.action-text {
    font-size: 16px;
    margin-bottom: 5px;
}

.name-text {
    font-size: 18px;
    font-weight: bold;
    margin-bottom: 5px;
}

.title-contain-author {
    background-color: #f0f0f0;
    /* 背景颜色为灰色 */
    padding: 5px;
    /* 添加一些内边距 */
}

.avatar-and-username {
    display: flex;
    align-items: center;
}

.contain-text {
    font-size: 15px;
    margin-bottom: 5px;
}

.author-text {
    font-size: 14px;
    text-align: right;
    margin-top: 1px;
}

.dynamic-time {
    font-size: 12px;
    color: #999;
}

.avatar-and-author {
    display: flex;
    justify-content: flex-end;
}

.author-avatar {
    width: 25px;
    height: 25px;
    margin-right: 5px;
}

/* author头像 */

.scroll-to-top-btn {
    position: fixed;
    bottom: 20px;
    right: 20px;
    width: 50px;
    height: 50px;
    cursor: pointer;
    overflow: hidden;
    z-index: 999;
}

.image-container img {
    width: 100%;
    height: 100%;
    display: block;
    transition: opacity 0.3s;
}

.text-overlay {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    opacity: 0;
    pointer-events: none;
    transition: opacity 0.3s;
}

.scroll-to-top-btn:hover .image-container img {
    opacity: 0;
}

.scroll-to-top-btn:hover .text-overlay {
    opacity: 1;
}

.line {
    color: black;
    font-size: 16px;
}
</style>