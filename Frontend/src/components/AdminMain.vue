<script>
import { ElMessage, ElMessageBox } from 'element-plus';
export default {
    data() {
        return {
           announcement:'',
        };
    },
    methods:{
        clearAnnouncementInput()
        {
            this.announcement='';
        },
        subbmitAnnouncement()
        {

        }
    }
}
</script>

<template>
    <div class="common-layout bkBox">
      <el-container>
        <el-header>
            <div class="nav-left">
                <span class="header-tittle-text">足球系统管理员界面</span>
            </div>
            <div class="nav-right">
                <el-dropdown trigger="hover">
                    <span class="user-avatar" @click="redirectToPersonal">
                        <img src="../assets/img/football_logo.png" alt="Avatar" />
                    </span>
                    <template #dropdown>
                        <el-dropdown-menu v-slot: dropdown>
                        <el-dropdown-item @click="redirectToLogin">登录</el-dropdown-item>
                        <el-dropdown-item @click="redirectToRegister">注册</el-dropdown-item>
                        <el-dropdown-item @click="redirectToPersonal">个人中心</el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
                <span class="user-nickname">user-nickname</span>
            </div>
        </el-header>
        <el-container>
          <el-aside>
            <el-container class="aside-upper-box">
                被举报用户处理
            </el-container>
            <el-container class="aside-lower-box">
                <!--发布公告-->
                <el-container style="height:20%;width:100%;text-align: center;position: absolute;"><p class="announcement-tittle">发布公告</p></el-container>
                <el-container style="height:50%;width:100%;position: absolute;top:10vh;">
                    <el-input  
                        class="announcement-input-box"
                        v-model="announcement"
                        :rows="8"
                        type="textarea"
                        placeholder="请输入公告内容"
                    />
                </el-container>
                <el-container style="height:30%;width:100%;position: absolute;top:40vh;">
                    <el-button class="announcement-btn" style="margin-left:1vw;" @click=" clearAnnouncementInput()">
                        <span class="announcement-btn-text">清空</span>
                    </el-button>
                    <el-button class="announcement-btn" >
                        <span class="announcement-btn-text">发布</span>
                    </el-button>
                </el-container>
            </el-container>
          </el-aside>
            <el-container>
                <el-main>
                    <el-container class="main-upper-box">
                        被举报帖子处理
                    </el-container>
                    <el-container class="main-lower-box">
                        不知道有什么要处理
                    </el-container>
                </el-main>
          </el-container>
        </el-container>
      </el-container>
    </div>
  </template>  

<style scoped>
/*设置整个背景填充为淡灰色*/
.bkBox{
    background: #eee;
    position: absolute;
    height: 100%;
    width:100%;
}
/*头部导航栏*/
.el-header {
    background-color: white;
    position: relative;
    height: 10%;
    width: 100%;
    display: flex;
}
/*导航栏左侧容器*/
.nav-left {
    position: relative;
    height: 8vh;
    width:50%;
    display: flex;
    align-items: center;
}
.header-tittle-text{
    left: 10vw;
    position: relative;
    color: var(--gray-3, #828282);
    font-family: Nunito Sans;
    font-size: 2rem;
    font-style: normal;
    font-weight: 700;
    line-height: normal;
}
/*导航栏右侧容器*/
.nav-right {
    position: relative;
    height: 8vh;
    width:50%;
    display: flex;
    align-items: center;
}
.user-avatar {
    position: relative;
    display: inline-block;
    width: 40px;
    height: 40px;
    border-radius: 50%;
    overflow: hidden;
    right: -28vw;
}
.user-nickname {
    position: relative;
    right: -30vw;
}

/*左侧两栏*/
.el-aside{ 
    background-color: rgb(217, 143, 143);
    position: absolute;
    left:0;
    width: 35%;
    height:92%;
}
.aside-upper-box{
    margin-top: 5vh;
    margin-left:1vw;
    border-radius: 10px;
    position: relative;
    width:95%;
    height:30%;
    background-color: white;
}

/*发布公告栏*/
.aside-lower-box{
    margin-top: 2vh;
    margin-left:1vw;
    border-radius: 10px;
    position: relative;
    width:95%;
    height:57%;
    background-color: white;
}
.announcement-tittle{
    font-family: 'Courier New', Courier, monospace;
    color: #555555;
    font-weight: 2rem;
    font-size: 1.5rem;
    text-align: center;
    margin-top: 2vh;
    margin-left: 13vw;
    position: relative;
}
.announcement-input-box{
    margin-left: 1.5vw;
    position: relative;
    width:90%;
}
.announcement-btn{
    border-radius: 3rem;
    width:15vw;
    height:5vh;
    text-align: center;
    background-color: rgb(124, 119, 254);
}
.announcement-btn-text{
    font-family:'Gill Sans', 'Gill Sans MT', Calibri, 'Trebuchet MS', sans-serif;
    color: #ffffff;
    font-weight: 2rem;
    font-size: 1rem;
    position: relative;
}

/*右侧*/
.el-main{
    background-color: rgb(108, 165, 231);
    position: absolute;
    right:0;
    width: 65%;
    height:92%;
}
.main-upper-box{
    margin-top: 2.3vh;
    margin-right:1vw;
    border-radius: 10px;
    position: relative;
    width:98%;
    height:46%;
    background-color: white;
}
.main-lower-box{
    margin-top: 2vh;
    margin-right:1vw;
    border-radius: 10px;
    position: relative;
    width:98%;
    height:46%;
    background-color: white;
}
</style>