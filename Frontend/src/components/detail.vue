<script>
import MyNav from './nav.vue';
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';

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

            //用来判断几个按钮的初始情况
            isApproved:false,
            isCollected:false,
            isReported:false,
            isFollowed:false,

            //举报相关
            dialogFormVisible : false,
            formLabelWidth : '140px',
            report_descriptions:"",

            //帖子信息相关
            title:"",
            avatar:"",
            uName:"",
            uText:"",
            date:"",
            approvalNum:0,

            //评论相关
            jNames:[],
            jTexts:[],
            jDates:[],

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
            console.log("isAccount = " + this.isAccount);
        },
        analyse_date(date){
            // 创建一个 Date 对象来解析时间字符串
            const dateObject = new Date(date);
            // 提取年、月和日
            const year = dateObject.getFullYear();
            const month = String(dateObject.getMonth() + 1).padStart(2, "0"); // 月份是从 0 开始的，需要加1
            const day = String(dateObject.getDate()).padStart(2, "0");
            // 构建目标格式的日期字符串
            const formattedDate = `${year}-${month}-${day}`;
            return formattedDate;
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
                ElMessage.error("获取帖子信息失败");
            }else{
                this.title = response.data.title ;
                this.avatar = response.data.avatar;
                this.uName = response.data.name;
                this.uText = response.data.contains;
                
                this.date = this.analyse_date(response.data.publishDateTime);
                if(response.data.islike == 1)
                {
                    this.isApproved = true
                }
                if(response.data.iscollect == 1)
                {
                    this.isCollected = true
                }
                if(response.data.isfollow == 1)
                {
                    this.isFollowed = true
                }
                this.approvalNum = response.data.approvalNum;
                //this.judgers.jText.push()
            }
            console.log("response.data.islike = " + response.data.islike);
            console.log("response.data.iscollect = " + response.data.iscollect);
            console.log("response.data.isfollow = " + response.data.isfollow);
            console.log("response.data.approvalNum = " + response.data.approvalNum);
            response.data.comments.forEach(jInfo => {
                this.jNames.push(jInfo.userName);
                this.jTexts.push(jInfo.contains);
                this.jDates.push(this.analyse_date(jInfo.publishDateTime));
            });
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
                ElMessage.success("点赞成功");
            }else{
                ElMessage.error("点赞失败");
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
            if(response.data.ok=='yes'){
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
                ElMessage.success("收藏成功");
                this.isCollected = !this.isCollected;
            }else{
                ElMessage.error("收藏失败");
            }
        },
        async reportPost()
        {
            console.log("send report request")
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                     Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/Forum/report',  {
                    post_id: this.$route.query.clickedPostID,
                    descriptions:String(this.report_descriptions),
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
            console.log(response.data.ok)
            if(response.data.ok == 'yes')
            {
                ElMessage.success("举报成功");
                this.isFollowed = !this.isFollowed;
            }else if(response.data.ok == 'no'){
                ElMessage.error("举报失败");
            }
            this.report_descriptions="";
            //this.goBack();
        },
        async follow()
        {
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                     Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/Forum/follow',  {
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
                ElMessage.success("关注成功");
                this.isFollowed = !this.isFollowed;
            }else{
                ElMessage.error("关注失败");
            }
        },
        goBack()
        {
            if(this.$route.query.fromAdmin == 1){
                this.$router.push('/AdminMain');    
            }else{
                this.$router.push('/forum');
            }
        }
    }
}
</script>

<template>
    <div class="common-layout">
        <my-nav/>
        <el-container class="post-container">
            <el-header>
                <el-dialog v-model="this.dialogFormVisible" title="举报详情">
                <el-form :model="form">
                <el-form-item label="请填写举报理由" :label-width="formLabelWidth">
                    <el-input v-model="report_descriptions" autocomplete="off" />
                </el-form-item>
                </el-form>
                <template #footer>
                    <span class="dialog-footer">
                        <el-button @click="dialogFormVisible = false">
                            放弃举报
                        </el-button>
                        <el-button type="primary" @click="dialogFormVisible = false;reportPost()">
                            确认举报
                        </el-button>
                    </span>
                </template>
            </el-dialog>
                <el-page-header @back="goBack"></el-page-header>
                    <span class="header-text" >{{ title }}</span>
                    <div v-if="this.$route.query.fromAdmin == 1">
                        <el-button class="header-respond-btn" style="right:2vw" @click="collectPost()">
                            <span>删除帖子</span>
                        </el-button>
                    </div>
                    <div v-else>
                        <el-button class="header-respond-btn" style="right:12.3vw" @click="collectPost()">
                        <span v-if="isCollected == false">收藏</span>
                        <span v-if="isCollected == true">已收藏</span>
                        </el-button>
                        <el-button :class="isApproved == false?'header-respond-btn':'header-respond-btn-active'" id="approveBtn" @click="approvePost()">
                        <img style="height:2vh;width: 1vw;margin-right: 0.5vw;" src="../assets/img/approve.png">
                            点赞 {{ approvalNum }}
                        </el-button>
                        <el-button class="header-respond-btn" style="right:8.3vw" @click="this.dialogFormVisible = true">
                            <span v-if="isReported == false">举报</span>
                            <span v-if="isReported == true">已举报</span>
                        </el-button>
                    </div>
            </el-header>
            <el-container style="border: 1px solid #ccc;">
                <el-aside>
                    <img class="rooter-img" src={{avatar}}>
                    <div class="rooter-name">
                        <p class="rooter-name-typography">{{ uName }}</p>
                    </div>
                    <el-container>
                        <el-button class="aside-follow-btn" @click="follow()">
                            <span v-if="isFollowed == false">关注</span>
                            <span v-if="isFollowed == true">已关注</span>
                        </el-button>
                    </el-container>
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
                        <div v-for="(jName,index) in jNames"> 
                            <el-divider style="color: black;height:2vw"></el-divider>
                            <p><text style="color: rgb(24, 151, 235);">{{ jName }} ：</text><text>{{ jTexts[index] }}</text></p>
                            <p style="top:3vw">{{ jDates[index] }}</p>
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
	margin-top: -10vw;
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
    left:0vw;
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

.aside-follow-btn{
    text-align: center;
    background-color: aliceblue;
    position: relative;
    left:5vw;
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

/*发布评论*/
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

/*左上角返回箭头*/
.el-page-header{
    position: relative;
    top:1.2vw;
}

.el-button--text {
  margin-right: 15px;
}
.el-select {
  width: 300px;
}
.el-input {
  width: 300px;
}
.dialog-footer button:first-child {
  margin-right: 10px;
}
</style>
