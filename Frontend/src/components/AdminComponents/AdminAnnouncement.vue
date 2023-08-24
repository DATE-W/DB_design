<script>
import axios from 'axios';
import { ElMessage } from 'element-plus';
    export default {
    components:{
      
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
        }
    }
}
</script>

<template>
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
            <el-button class="announcement-btn" style="margin-left:1vw;" @click=" clearAnnouncementInput">
                <span class="announcement-btn-text">清空</span>
            </el-button>
            <el-button class="announcement-btn" @click="subbmitAnnouncement">
                <span class="announcement-btn-text">发布</span>
            </el-button>
        </el-container>
    </el-container>
</template>

<style scoped>
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
</style>