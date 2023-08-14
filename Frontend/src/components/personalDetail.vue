<!-- 我的动态 v1.0 -->
<template>
    <div class="overflow-container">
        <div class="bg-theme">
            <img src="../assets/img/carousel1.png" alt="Carousel Image" class="bg-image">
        </div>
        <!-- 上方展示背景图 -->
        <el-container class="main-container">
            <el-main class="user-profile">
                <!-- 遍历动态分组数组，为每个分组创建区域 -->
                <div class="dynamic-group" v-for="(group, groupIndex) in groupedDynamics" :key="groupIndex">
                    <!-- 遍历分组内的动态，为每个动态创建区域 -->
                    <div class="dynamic-item" v-for="(dynamic, index) in group" :key="index">
                        <!-- 显示动态的具体内容 -->
                        <div class="dynamic-content">
                            <!-- 添加 contain 元素 -->
                            <!-- 第一行：用户行为 -->
                            <div class="action-text">{{ getActionText(dynamic) }}</div>
                            <div
                                :class="{ 'title-contain-author': dynamic.object.type !== 'user', 'user-section': dynamic.object.type === 'user' }">
                                <!-- 如果是用户则不用title-contain-author的样式 -->
                                <!-- 第二行：用户名+头像或帖子名 -->
                                <div class="avatar-and-username">
                                    <el-avatar v-if="dynamic.object.type === 'user'" :src="getUserAvatar(dynamic)"
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
                                {{ dynamic.time }}
                            </div>
                        </div>
                    </div>
                </div>
            </el-main>
            <el-aside class="right-profile">

            </el-aside>
        </el-container>
        <!-- 展示我的动态 -->
    </div>
</template>

<script>
export default {
    data() {
        return {
            dynamics: [
                {
                    type: 'like',
                    time: '2023-08-10',
                    object: {
                        type: 'post',
                        title: '介绍一下新一期深渊的配队',
                        contain: '原神电脑端的画风，真的完美符合我对二次元的影响，开服玩的手机端，两天就卸载了，一年后游戏荒，又在电脑下了...',
                        author: '门酱'
                    }
                },
                {
                    type: 'like',
                    time: '2023-08-10',
                    object: {
                        type: 'post',
                        title: '会玩的界孙权薄纱神甘宁',
                        contain: '曾经有教育家做了一个实验，给魏蜀国孩子和吴国孩子一手牌，让他们不用制冷器就让水结成冰。吴国孩子玩界孙权...',
                        author: '我直接大制衡'
                    }
                },
                {
                    type: 'follow',
                    time: '2023-08-09',
                    object: {
                        type: 'user',
                        username: 'tilitili直播'
                    }
                },
                {
                    type: 'comment',
                    time: '2023-08-08',
                    object: {
                        type: 'comment',
                        title: '帖子',
                        contain: '我三岁练枪，那一年枪一上手就人枪合一😎爱不释手，九岁悟出夺命十三枪😤于九天之上我斩杀花果山妖猴😠...',
                        author: 'superwh'
                    }
                },
            ]
        };
    },
    computed: {
        // 将动态按时间分组的计算属性
        groupedDynamics() {
            const groups = {};
            this.dynamics.forEach(dynamic => {
                if (!groups[dynamic.time]) {
                    groups[dynamic.time] = [];
                }
                groups[dynamic.time].push(dynamic);
            });
            return Object.values(groups);
        }
    },
    methods: {
        // 根据动态类型返回相应的文本内容
        getActionText(dynamic) {
            switch (dynamic.type) {
                case 'like':
                    return `您赞同了帖子`;
                case 'follow':
                    return `您关注了用户`;
                case 'comment':
                    return `您评论了帖子`;
                default:
                    return '未知操作';
            }
        },
        // 获取用户名或帖子名
        getNameText(dynamic) {
            return dynamic.object.username || dynamic.object.title;
        },
        getUserAvatar(dynamic) {
            if (dynamic.object.type === 'user') {
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
    height: 200px;
}

.bg-image {
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    object-fit: cover;
}

.right-profile {
    width: 20vw;
    background-color: aliceblue;
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

.avatar-and-author{
    display: flex;
    justify-content: flex-end;
}

.author-avatar {
    width: 25px;
    height: 25px;
    margin-right: 5px;
}/* author头像 */
</style>