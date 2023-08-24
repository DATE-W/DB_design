<script>
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
    export default {
    components:{
      
    },
    data() {
        return {
            Users: [
                { user_id: 0,userName: "U 1",userPoint: 0,regDate: "2023-08-17",followedNumber:0 },
            ],
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
            console.log("response = "+response)
            console.log("JSON.stringify(response) = "+JSON.stringify(response, null, 2))
            console.log("JSON.stringify(response.data) = "+JSON.stringify(response.data, null, 2))
            console.log("response.data = "+response.data)
            console.log("response.data.ok = "+response.data.ok)
            console.log("response.data.value = "+response.data.value)

            if(response.data.ok=='no')
            {
                ElMessage.error("获取用户列表失败");
            }else if(response.data.ok=='yes'){
               // 遍历传来的数据并进行转换
                response.value.forEach(item => {
                    const convertedItem = {
                        user_id: item.user_id,
                        userName: item.userName,
                        userPoint: item.userPoint,
                        regDate: item.regDate,
                        followedNumber: item.followedNumber || 0
                    };
                // 将转换后的数据添加到 reportedPost 数组中
                this.Users.push(convertedItem);
                });
            }
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
        },
    }
}
</script>

<template>
    <el-container class="main-lower-box">
        <el-table :data="Users" border height="300" style="width: 100%;border-radius: 10px;">
            <el-table-row label="111"/>
            <el-table-column align="center" prop="user_id" label="Id" width="100" />
            <el-table-column prop="userName" label="昵称" width="150" />
            <el-table-column align="center" prop="regDate" label="注册时间" width="200" />
            <el-table-column align="center" prop="userPoint" label="积分" width="150" />
            <el-table-column align="center" prop="followedNumber" label="粉丝数" width="150" />
            <el-table-column fixed="right" label="操作">
                <template #default="scope">
                        <el-button link type="primary" size="small" @click="deleteUser(scope.$index)">封禁用户</el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-container>
</template>

<style scoped>
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