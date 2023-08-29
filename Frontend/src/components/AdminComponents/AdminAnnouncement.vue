<script>
import axios from 'axios';
import { ElMessage } from 'element-plus';
import dashboard from './AdminDashBoard.vue';
import headerview from './AdminNav.vue';
export default {
    components:{
        dashboard:dashboard,
        headerview:headerview,
    },
    data() {
        return {
           announcement:'',
        };
    },
    methods:{
        clearAnnouncementInput()
        {
            this.announcement='';
            return
        },
        async subbmitAnnouncement()
        {
            console.log("subbmitAnnouncement "+ this.announcement)
            let response
            try {
                response = await axios.post('api/notice/createNotice',  {
                    admin_id:0,
                    text:String(this.announcement),
                });
                console.log(JSON.stringify(response.data))
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
            console.log(response);
            if (response.data.ok == 'yes') {
                ElMessage.success("发布成功");
            }
            else {
               ElMessage.error("发布失败");
            }
            this.announcement='';
            return
        }
    }
}
</script>

<template>
    <div id="building">
        <el-container class="rooter-box">
        <el-header class="hide-header">
            <headerview/>
        </el-header>
        <el-container>
            <el-aside width="20vw" class="hide-aside">
            <dashboard/>
            </el-aside>
            <el-main style="background-color:white;margin-top: 2vh;margin-left: 0.7vw;border-radius: 15px 15px 0 0;display: flex;flex-direction: column;justify-content: center;">
                <span class="announcement-tittle">发布公告</span>
                <el-container style="height:20vh;position: relative;top:10vh;left:3vw;">
                    <el-input  
                        class="announcement-input-box"
                        v-model="announcement"
                        :rows="8"
                        type="textarea"
                        placeholder="请输入公告内容"
                    />
                </el-container>
                <el-container style="height:20vh;position: relative;left:13vw;">
                    <el-button class="announcement-btn" @click=" clearAnnouncementInput">
                        <span class="announcement-btn-text">清空</span>
                    </el-button>
                    <el-button class="announcement-btn" @click="subbmitAnnouncement">
                        <span class="announcement-btn-text">发布</span>
                    </el-button>
                </el-container>
            </el-main>
        </el-container>
        </el-container>
    </div>
</template>

<style scoped>
@media (max-width: 768px) { /* 设置适当的最大宽度值 */
  .hide-aside {
    display: none;
  }
  .hide-header {
    display: none;
  }
}

#building{
    background-color:#eee;
    left:0px;
    top:0px;
    width:100vw;			
    height:100vh;		
    position: fixed;
}

.rooter-box{
    position: fixed;
    width:80vw;
    height:100vh;
    left: 10vw;
}
/*发布公告栏*/
.announcement-tittle{
    font-family: 'Courier New', Courier, monospace;
    color: #555555;
    font-weight: 2rem;
    font-size: 1.5rem;
    text-align: center;
    margin-top: 2vh;
    position: relative;
}
.announcement-input-box{
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
</style>