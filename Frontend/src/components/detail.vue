<script>
import MyNav from './nav.vue';
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
import { ArrowLeft } from '@element-plus/icons-vue';
export default {
    components:{
        'my-nav': MyNav
    },
    mounted() {
        this.JudgeAccount();
        this.GetPostDetail();
        console.log("post_id = " + this.$route.query.clickedPostID);
    },
    data(){
        return{

            isAccount:false,

            isApproved:false,
            isCollected:false,
            title:"",
            uImg:"这是头像",
            uName:"",
            uText:"",
            date:"",
            approvalNum:0,

            judgers:[
                {jName:"草神和伞兵什么时候结婚",jText:"你说得对，但是原神是一款",jDate:"2023-07-08"},
                {jName:"丁真Official",jText:"义！乌！",jDate:"2023-07-09"},
                {jName:"杰子",jText:"同学报一下学号姓名给你加创新学分",jDate:"2023-07-10"},
            ],

            userJudge:"",
        }
    },
    methods:{
        async JudgeAccount() {
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                    Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/UserToken', {}, { headers })
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
            console.log("isAccount = " + this.isAccount);
        },
        async GetPostDetail()
        {
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                     Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/Forum/PostInfo',  {
                    post_id: this.$route.query.clickedPostID,
                }, { headers });
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
                }
                return
            }
            if(response.data.ok=='no')
            {
                ElMessage.error(response.data.value);
            }else{
                this.title = response.data.title ;
                //uImg
                this.uName = response.data.name;
                this.uText = response.data.contains;
                this.date = response.data.publishDateTime;
                if(response.data.islike == 1)
                {
                    this.isApproved = true
                }
                if(response.data.iscollect == 1)
                {
                    this.isCollected = true
                }
                this.approvalNum = response.data.approvalNum;
            }
            console.log("response.data.islike = " + response.data.islike);
            console.log("response.data.iscollect = " + response.data.iscollect);
            console.log("response.data.approvalNum = " + response.data.approvalNum);
            console.log("response = " + response);
            console.log("isApproved1 = " + this.isApproved);
        },
        async approvePost()
        {
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                     Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/Forum/Like',  {
                    post_id: this.$route.query.clickedPostID,
                }, { headers });
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
                }
                return
            }
            if(response.data.ok == 'yes')
            {
                ElMessage.success(response.data.value);
            }else{
                ElMessage.error(response.data.value);
            }
            this.isApproved = !this.isApproved;
            if(this.isApproved==false){
                this.approvalNum--;
            }else{
                this.approvalNum++;
            }
            console.log("isApproved2 = " + this.isApproved);
        },
        async PostJudge()
        {
            console.log('userJudge = ' + this.userJudge);
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                     Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/Forum/NewComment',  {
                    post_id: this.$route.query.clickedPostID,
                    contains: String(this.userJudge),
                }, { headers });
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
                }
                return
            }
            console.log("发送评论 " + response.data.value);
            this.userJudge="";
            if(response.data.ok=='ok'){
                location.reload();
            }
        },
        async collectPost()
        {
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                     Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/Forum/collect',  {
                    post_id: this.$route.query.clickedPostID,
                }, { headers });
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
                }
                return
            }
            if(response.data.ok == 'yes')
            {
                ElMessage.success(response.data.value);
            }else{
                ElMessage.error(response.data.value);
            }
            this.isCollected = !this.isCollected;
        },
        goBack()
        {
            this.$router.push('/forum');
        }
    }
}
</script>

<template>
    <div class="common-layout">
        <my-nav/>
        <el-container class="post-container">
            <el-header>
                <el-page-header :icon="ArrowLeft" @back="goBack">
                    <span class="header-text" >{{ title }}</span>
                    <el-button class="header-respond-btn" style="right:8.3vw" @click="collectPost()">
                        <span v-if="isCollected == false">收藏</span>
                        <span v-if="isCollected == true">已收藏</span>
                    </el-button>
                    <el-button :class="isApproved == false?'header-respond-btn':'header-respond-btn-active'" id="approveBtn" @click="approvePost()">
                    <img style="height:2vh;width: 1vw;margin-right: 0.5vw;" src="../assets/img/approve.png">
                        点赞 {{ approvalNum }}
                    </el-button>
                </el-page-header>
            </el-header>
            <el-container style="border: 1px solid #ccc;">
                <el-aside>
                    <!-- <img class="rooter-img" src=""> -->
                    <div class="rooter-img">{{ uImg }}</div>
                    <div class="rooter-name">
                        <p class="rooter-name-typography">{{ uName }}</p>
                    </div>
                </el-aside>
                <el-main>
                    <!-- 主帖 -->
                    <div class="rooter-post">
                        {{ uText }}
                        <div class="bottom-detail">
                            <div>{{ date }}</div>
                        </div>
                    </div>
                    <!-- 评论 -->
                    <div class="judger-post">
                        <div v-for="judger in judgers"> 
                            <el-divider style="color: black;height:2vw"></el-divider>
                            <p><text style="color: rgb(24, 151, 235);">{{ judger.jName }} ：</text><text>{{ judger.jText }}</text></p>
                            <p style="top:2vw">{{ judger.jDate }}</p>
                        </div>
                    </div>
                </el-main>
            </el-container>
        </el-container>
        <div class="input-respond-container">
            <div style="margin: 20px 0">
                <el-input
                    v-model="userJudge"
                    :rows="5"
                    type="textarea"
                    placeholder="和大家分享你的看法~"
                />
                <el-button class="input-respond-btn" @click="PostJudge()">发布评论</el-button>
            </div>
        </div>
    </div>
</template>

<style scoped>
/*帖子的整体容器*/
.post-container{
    width:70vw;
    height:80vw;
	background: #eee;
	position: absolute;
	left: 20%;top: 30%;
	margin-left: -5vw;
	margin-top: -5vw;
}
/*头部帖子标题+收藏/回复按钮*/
.el-header{
    text-align: left;
}
.header-text{
    font-family: SimHei;
    font-size: 1.5rem;
    font-weight: 200;
    position: absolute;
    top:1vw;
    left: 7vw;
}
.header-respond-btn{
    text-align: center;
    background-color: aliceblue;
    position: absolute;
    top:1vw;
    right: 2vw;
}
.header-respond-btn-active
{
    text-align: center;
    background-color: rgb(255, 201, 248);
    position: absolute;
    top:1vw;
    right: 2vw;
}
/*左侧楼主信息栏*/
.el-aside{
    width:15vw;
    background-color: rgb(230, 230, 230);
    text-align: center;
}
/*楼主信息 头像+昵称*/
.rooter-img{
    position: relative;
    width:6vw;
    height:6vw;
    top:2vw;
    bottom:2vw;
    left:4vw;
    background-color: bisque;
}
.rooter-name{
    position: relative;
    width:7vw;
    height:7vw;
    top:2.5vw;
    bottom:2vw;
    left:3.5vw;
}
.rooter-name-typography{
    font-family: KaiTi;
    font-size:1.25rem;
    color:rgb(41, 93, 151);
}
/*主信息板块*/
.el-main{
    text-align: left;
    padding-left: 3vw;
    padding-right: 3vw;
}
.bottom-detail{
    width:100%;
    height:10vw;
    position: relative;
    top:3vw;
    bottom: 0;
    right:0vw;
}
.input-respond-container{
    background-color: rgb(251, 249, 246);
    position: fixed;
    bottom: 0;
    width:70vw;
    height:13vw;
    left: 20%;
	margin-left: -5vw;
}
.input-respond-btn{
    text-align: center;
    background-color:#6fc8e3;
    position: relative;
    margin-top:1vw;
    right:-60vw;
}

.el-page-header{
    top:1vw;
}
</style>
