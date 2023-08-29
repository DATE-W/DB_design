<script>
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
import dashboard from './AdminDashBoard.vue';
import headerview from './AdminNav.vue';
export default {
    components:{
        dashboard:dashboard,
        headerview:headerview,
    },
    data() {
        return {
            Users: [],
        };
    },
    mounted(){
        this.getAllUsers();
    },
    methods:{
        async getAllUsers()
        {
            console.log("start get all users")
            let response
            try {
                response = await axios.get('api/report/getUsrInfo');
            } catch (err) {
                console.error(err);
                if (err.response.data.result == 'fail') {
                    ElMessage.error(err.response.data.msg)
                } else {
                    ElMessage.error("未知错误")
                }
                return
            }
            console.log("AllUsers - JSON.stringify(response) = "+JSON.stringify(response, null, 2))
            if(response.data.ok=='no')
            {
                ElMessage.error("获取用户列表失败");
            }else if(response.data.ok=='yes'){
               // 遍历传来的数据并进行转换
                response.data.value.forEach(item => {
                    const convertedItem = {
                        user_id: item.user_id,
                        userName: item.userName,
                        userPoint: item.userPoint,
                        regDate: item.regDate,
                        followedNumber: item.followedNumber || 0
                    };
                // 将转换后的数据添加到 reportedPost 数组中
                this.Users.push(convertedItem);
                 // 对 Users 数组按照 user_id 进行排序
                this.Users.sort((a, b) => a.user_id - b.user_id);
                });
            }
            return
        },
        async deleteUser(index)
        {
            console.log("deleteUser")
            console.log("index " + index);
            let response
            try {
                response = await axios.post('api/report/banUsr',  {
                    user_id:this.Users[index].user_id,
                });
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
            console.log("JSON.stringify(response.data) = "+JSON.stringify(response, null, 2))
            console.log("response.data.value = " + response.data.value);
            if (response.data.ok == 'yes') {
                ElMessage.success("封禁用户成功");
            }
            else {
               ElMessage.error("未找到相关用户信息");
            }
            return
        },
    }
}
</script>

<template>
    <!-- <el-container class="main-lower-box">
        <el-table :data="Users" border height="300" style="width: 100%;border-radius: 10px;">
            <el-table-row label="111"/>
            <el-table-column align="center" prop="user_id" label="用户Id" width="100" />
            <el-table-column prop="userName" label="用户昵称" width="150" />
            <el-table-column align="center" prop="regDate" label="注册时间" width="200" />
            <el-table-column align="center" prop="userPoint" label="积分" width="150" />
            <el-table-column align="center" prop="followedNumber" label="粉丝数" width="150" />
            <el-table-column fixed="right" label="操作">
                <template #default="scope">
                        <el-button link type="primary" size="small" @click="deleteUser(scope.$index)">封禁用户</el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-container> -->
    <div id="building">
        <el-container class="rooter-box">
        <el-header class="hide-header">
            <headerview/>
        </el-header>
        <el-container>
            <el-aside width="20vw" class="hide-aside">
            <dashboard/>
            </el-aside>
            <el-main style="overflow-y: auto;background-color:white;margin-top: 2vh;margin-left: 0.7vw;border-radius: 15px 15px 0 0;">
            <el-container>
                撰写用户板块
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
</style>

<!-- <style scoped>
.main-lower-box{
    margin-top: 2vh;
    margin-right:1vw;
    border-radius: 10px;
    position: relative;
    width:98%;
    height:46%;
    background-color: white;
}

</style> -->