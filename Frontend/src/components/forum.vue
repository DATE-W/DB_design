<!-- 论坛界面v2.2 -->
<template>
    <div class="common-layout">
        <el-container class="bkBox">
            <my-nav></my-nav>
            <el-header class="nav-header">
                <!-- 点击了某个联赛 -->
                <div :class="['forum-header', selectedColor]" v-if="isButtonClicked">
                    <p class="subTittle">{{ selectedTopic }}</p>
                    <p class="subDescription">{{ selectedDescription }}</p>
                </div>
                <!-- 尚未点击任何按钮 -->
                <div class="subHeaderBox" v-else>
                    <p class="subTittle">论坛</p>
                    <p class="subDescription">欢迎来到论坛</p>
                </div>
            </el-header>
            <!-- 上端论坛话题显示 -->
            <el-container class="content-container">
                <el-aside width="20vw">
                    <div class="button-container">
                        <el-button class="postButton" @click="redirectToEditPost" round>
                            <text class="postButtonText">{{ isAccount ? '点此发帖' : '点此登录' }}</text>
                        </el-button>
                        <div v-for="row in buttonRows" :key="row">
                            <el-button v-for="button in row" :key="button.type" @click="selectTopic(button)"
                                class="raceButtonBox" text>
                                <div :class="['raceButtonCircle', button.color]"></div>
                                <div class="raceButtonText">{{ button.text }}</div>
                            </el-button>
                        </div>
                    </div>
                </el-aside>
                <!-- 左侧选择话题 -->
                <el-main class="main-container">
                    <div class="up-container">
                        <el-icon class="search-icon">
                            <Search />
                        </el-icon>
                        <el-input class="search-input" v-model="searchKeyword" placeholder="请输入关键词"
                            @keyup.enter.native="SearchForPosts(1, this.pageSize, this.currentTag, this.searchKeyword)"></el-input>
                        <el-button v-if="ShowReturnButton" icon="ArrowLeft" @click="ReturnToBegin"></el-button>
                        <div class="sort-container">
                            <el-button v-if="ShowSortButtons" :class="{ 'sort-button': sortbytime }"
                                @click="changeSort('time')">按时间排序</el-button>
                            <el-button v-if="ShowSortButtons" :class="{ 'sort-button': sortbylike }"
                                @click="changeSort('like')">按热度排序</el-button>
                        </div>
                    </div>
                    <div class="post-list">
                        <template v-for="(title, index) in post_title">
                            <hr v-if="index == 0" class="separator">
                            <div :class="['post-box', 'normal-post-box']" @click="goToDetail(post_id[index])">
                                <div class="post-title">{{ title }}</div>
                                <div class="post-contain">{{ post_contains[index] }}</div>
                                <div class="post-author">
                                    <el-icon>
                                        <Avatar />
                                    </el-icon>
                                    {{ post_author[index] }}
                                </div>
                            </div>
                            <hr v-if="index <= post_title.length - 1" :key="`separator-${index}`" class="separator">
                        </template>
                    </div>
                    <div class="page-container" v-if="showPage">
                        <el-pagination @current-change="handlePageChange" :current-page="currentPage" :page-size="pageSize"
                            layout="prev, pager, next, jumper" :total="totalPosts"></el-pagination>
                    </div>
                    <!-- 换页(含跳转功能) -->
                </el-main>
                <!-- 右侧展示当前话题的帖子 -->
            </el-container>
        </el-container>
    </div>
</template>
  
<script>
import MyNav from './nav.vue';
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
export default {
    components: {
        'my-nav': MyNav
    },
    data() {
        return {
            isAccount: false,
            selectedTopic: '论坛',
            selectedColor: '',  //随着选择的主题改变颜色
            selectedDescription: '',  //随着选择的主题改变描述
            searchKeyword: '', // 保存搜索关键词
            isButtonClicked: false,
            buttons: [
                { type: 'ALL', text: '全部', color: 'ALL', description: '浏览所有联赛相关' },
                { type: 'LL', text: '西甲', color: 'LL', description: '西甲联赛是西班牙的顶级足球联赛' },
                { type: 'PL', text: '英超', color: 'PL', description: '英超联赛是英格兰的顶级足球联赛' },
                { type: 'BL', text: '德甲', color: 'BL', description: '德甲联赛是德国的顶级足球联赛' },
                { type: 'SA', text: '意甲', color: 'SA', description: '意甲联赛是意大利的顶级足球联赛' },
                { type: 'L1', text: '法甲', color: 'L1', description: '法甲联赛是法国的顶级足球联赛' },
                { type: 'CSL', text: '中超', color: 'CSL', description: '中超联赛是中国建立的足球联赛，拥有武磊等顶级球员' },
            ],
            sortbytime: true,
            sortbylike: false,
            ShowReturnButton: false,
            ShowSortButtons: true,
            currentPage: 1,
            pageSize: 4,  //每页4项
            totalPosts: 0,
            showPage: false, //初始为false 向后端请求完数据后变为true 更换tag页面暂时变为false
            post_id: [],  //存储返回的帖子id
            post_title: [],  //存储返回的帖子标题
            post_contains: [],  //存储返回的帖子内容
            post_author: [],  //存储返回的帖子作者
            post_time: [],  //存储返回的发帖时间
            currentTag: 'ALL',  //向后端传递当前页面的帖子类型 初始为全部 不受tag影响
        };
    },
    computed: {
        buttonRows() {
            const rows = [];
            const buttonsPerRow = 1;
            for (let i = 0; i < this.buttons.length; i += buttonsPerRow) {
                const row = this.buttons.slice(i, i + buttonsPerRow);
                rows.push(row);
            }
            return rows;
        },
    },
    mounted() {
        this.JudgeAccount();
        this.getPostsbyTime(1, this.pageSize, this.currentTag);
        this.getPostNum();//获取所有的帖子的总数
    },
    methods: {
        selectTopic(button) {
            this.isButtonClicked = true;
            this.selectedTopic = button.text;
            this.selectedColor = button.color;
            this.selectedDescription = button.description;
            this.currentTag = button.type === 'ALL' ? 'ALL' : button.text;
            if (this.sortbytime)
                this.getPostsbyTime(1, this.pageSize, this.currentTag);
            else
                this.getPostsbyLike(1, this.pageSize, this.currentTag);
        },
        //选择对应的标签 同时修改论坛上方的展示内容
        redirectToEditPost() {
            if (this.isAccount)
                this.$router.push('/EditPost');
            else {
                this.$router.push('/signin');
            }
        },
        handlePageChange(currentPage) {
            this.currentPage = currentPage;
            if (this.sortbytime)
                this.getPostsbyTime(this.currentPage, this.pageSize, this.currentTag);
            else
                this.getPostsbyLike(this.currentPage, this.pageSize, this.currentTag);
        },
        goToDetail(postId) {
            console.log(postId);
            this.$router.push({
                path: '/detail',
                query: { clickedPostID: postId }
            });
        },
        changeSort(button) {
            if (button === 'time') {
                if (this.sortbylike)
                    this.getPostsbyTime(1, this.pageSize, this.currentTag);
                this.sortbytime = true;
                this.sortbylike = false;
            }
            else if (button === 'like') {
                if (this.sortbytime)
                    this.getPostsbyLike(1, this.pageSize, this.currentTag);
                this.sortbytime = false;
                this.sortbylike = true;
            }
        },
        async JudgeAccount() {
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                    Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/UserToken/UserToken', {}, { headers })
            } catch (err) {
                if (err.response.data.result == 'fail') {
                    ElMessage({
                        message: err.response.data.msg,
                        grouping: false,
                        type: 'error',
                    })
                } else {
                    ElMessage({
                        message: '未知错误',
                        grouping: false,
                        type: 'error',
                    })
                    return
                }
                return
            }
            if (response.data.ok == 'yes') {
                this.isAccount = true;
            }
            else {
                this.isAccount = false;
            }
        },
        async getPostNum() {
            let response
            try {
                response = await axios({
                    method: 'GET',
                    url: '/api/Forum/GetPostNum',
                })
            } catch (err) {
                ElMessage({
                    message: '获取帖子总数失败',
                    grouping: false,
                    type: 'error',
                })
            }
            //console.log(response.data.totalPostsCount);
            if (response.data.totalPostsCount) {
                this.totalPosts = response.data.totalPostsCount; // 将帖子的总数保存到data中的totalItems中
                this.showPage = true;
            } else {
                ElMessage({
                    message: '后端返回的帖子总数格式错误',
                    grouping: false,
                    type: 'error',
                });
            }
        },
        async getPostsbyTime(pageNumber, pageSize, currentTag) {
            let response
            try {
                response = await axios.post('/api/Forum/GetPostbyOrder', {
                    page: pageNumber,
                    count: pageSize,
                    tag: String(currentTag),
                }, {})
            } catch (err) {
                ElMessage({
                    message: '获取帖子失败',
                    grouping: false,
                    type: 'error',
                });
            }
            //console.log('response:', response);
            this.post_id = [];
            this.post_title = [];
            this.post_contains = [];
            this.post_author = [];
            this.totalPosts = response.data.count;
            if (response.data.postInfoJsons && Array.isArray(response.data.postInfoJsons)) {
                response.data.postInfoJsons.forEach((postInfo) => {
                    this.post_id.push(postInfo.post_id);
                    this.post_title.push(postInfo.title);
                    this.post_contains.push(postInfo.contains);
                    this.post_author.push(postInfo.author);
                });
            }
            else {
                ElMessage({
                    message: '后端返回的帖子数据格式错误',
                    grouping: false,
                    type: 'error',
                });
            }
        },
        async getPostsbyLike(pageNumber, pageSize, currentTag) {
            let response
            try {
                response = await axios.post('/api/Forum/GetPostbyLike', {
                    page: pageNumber,
                    count: pageSize,
                    tag: String(currentTag),
                }, {})
            } catch (err) {
                ElMessage({
                    message: '获取帖子失败',
                    grouping: false,
                    type: 'error',
                });
            }
            this.post_id = [];
            this.post_title = [];
            this.post_contains = [];
            this.post_author = [];
            this.totalPosts = response.data.count;
            if (response.data.postInfoJsons && Array.isArray(response.data.postInfoJsons)) {
                response.data.postInfoJsons.forEach((postInfo) => {
                    this.post_id.push(postInfo.post_id);
                    this.post_title.push(postInfo.title);
                    this.post_contains.push(postInfo.contains);
                    this.post_author.push(postInfo.author);
                });
            }
            else {
                ElMessage({
                    message: '后端返回的帖子数据格式错误',
                    grouping: false,
                    type: 'error',
                });
            }
        },
        async SearchForPosts(pageNumber, pageSize, currentTag, keyword) {
            this.ShowReturnButton = true;
            this.ShowSortButtons = false;
            let response
            try {
                response = await axios.post('/api/Forum/GetPostbySearch', {
                    page: pageNumber,
                    count: pageSize,
                    tag: String(currentTag),
                    key: String(keyword),
                }, {})
            } catch (err) {
                ElMessage({
                    message: '获取帖子失败',
                    grouping: false,
                    type: 'error',
                });
            }
            this.post_id = [];
            this.post_title = [];
            this.post_contains = [];
            this.post_author = [];
            this.totalPosts = response.data.count;
            if (response.data.postInfoJsons && Array.isArray(response.data.postInfoJsons)) {
                response.data.postInfoJsons.forEach((postInfo) => {
                    this.post_id.push(postInfo.post_id);
                    this.post_title.push(postInfo.title);
                    this.post_contains.push(postInfo.contains);
                    this.post_author.push(postInfo.author);
                });
            }
            else {
                ElMessage({
                    message: '后端返回的帖子数据格式错误',
                    grouping: false,
                    type: 'error',
                });
            }
            console.log('搜索关键词:', this.searchKeyword);
        },
        ReturnToBegin() {
            this.ShowReturnButton = false;
            this.ShowSortButtons = true;
            if (this.sortbytime) {
                this.getPostsbyTime(1, this.pageSize, this.currentTag);
            } else {
                this.getPostsbyLike(1, this.pageSize, this.currentTag);
            }
        }
    },
};
</script>

<style scoped>
.bkBox {
    position: absolute;
    width: 99.5%;
    height: 98.9%;
    background-color: #F5F7FA;
}

.nav-header {
    margin-top: -20px;
}

.forum-header {
    text-align: center;
    height: 5.2vw;
}

.button-container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin-top: 18%;
}

.content-container {
    flex-grow: 1;
    display: flex;
    flex-direction: row;
}

/*次顶部显示style*/
.subHeaderBox {
    text-align: center;
    height: 5.2vw;
    background-color: #93eefc;
}

.subTittle {
    color: black;
    font-family: NSimSun;
    font-size: 1.8rem;
    font-style: normal;
    font-weight: 600;
    line-height: normal;
    height: 0.7vw;
}

.subDescription {
    color: black;
    font-family: STKaiti;
    font-size: 1.5rem;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    height: 0.7vw;
}

/* 发帖按钮 */
.postButton {
    margin-top: 1vw;
    margin-bottom: 0.3vw;
    border-radius: 8vw;
    background-color: #3e28e3;
    box-shadow: 1px 1px 5px 0px rgba(0, 0, 0, 0.25);
    width: 14vw;
    height: 2.5vw;
}

.postButtonText {
    color: rgb(255, 255, 255);
    font-family: Ubuntu;
    font-size: 1.2rem;
    font-style: normal;
    font-weight: 700;
    line-height: normal;
    letter-spacing: 0.8px;
}

/*各联赛按钮*/
.raceButtonBox {
    margin: 0.5vw;
    border-radius: 20vw;
    height: 1.5vw;
}

.raceButtonCircle {
    position: relative;
    left: -1.5vw;
    border-radius: 50%;
    width: 1.7vw;
    height: 1.7vw;
}

.raceButtonText {
    font-family: Ubuntu;
    font-size: 1rem;
    font-style: normal;
    font-weight: 600;
    line-height: normal;
    letter-spacing: 0.8px;
}

.up-container {
    margin-top: 40px;
    display: flex;
    align-items: center;
}

.search-input {
    width: 30%;
}

.search-icon {
    font-size: 24px;
}

.sort-container {
    margin-left: 575px;
}

.sort-button {
    color: orange;
}

.page-container {
    position: absolute;
    bottom: 0px;
    left: 55%;
    transform: translateX(-50%);
}

/* 将换页放在最底部的中央 */

.main-container {
    display: flex;
    flex-direction: column;
}

.post-list {
    margin-top: 10px;
    margin-bottom: 25px;
    flex-grow: 1;
    display: flex;
    flex-direction: column;
}

.post-box {
    line-height: normal;
    height: 25%;
    align-items: left;
    transition: background-color 0.2s ease;
    position: relative;
    justify-content: space-between;
    flex-direction: column;
    display: flex;
}

.post-title {
    font-size: 1.3rem;
    font-style: normal;
    font-weight: 400;
    color: #005ce6;
}

.post-contain {
    font-size: 1.0rem;
    font-style: normal;
    font-weight: 400;
}

.post-author {
    align-self: flex-end;
    align-items: center;
    font-size: 0.8rem;
    color: #888;
    margin-bottom: 2px;
}

.normal-post-box {
    background-color: #F5F7FA;
    /* 默认状态的背景颜色 */
}

.post-box:hover {
    background-color: #fff;
    /* 鼠标悬浮状态的背景颜色 */
}

.separator {
    width: 100%;
    border: 0.5px solid #ccc;
    margin: 10px auto;
}

/*每个联赛的按钮颜色*/
.LL {
    background: #a1c4fd 0%;
}

.PL {
    background: #fed6e3 100%;
}

.BL {
    background: #fff1eb 0%;
}

.SA {
    background: #d4fc79 0%;
}

.L1 {
    background: #accbee 0%;
}

.CSL {
    background: #f78ca0 0%;
}

.ALL {
    background: #a1c4fd 100%;
}
</style>
