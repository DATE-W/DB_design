<script>
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
    export default {
    components:{
      
    },
    data() {
        return {
            signInNum:0,
            newUsrNum:0,
            newPostNum:0,
        };
    },
    mounted(){
        this.getYesInfo();
    },
    methods:{
        async getYesInfo(){
            console.log("start get Yesterday Info")
            let response
            try {
                response = await axios.get('/api/Post/getYestdayInfo');
            } catch (err) {
                console.error(err);
                if (err.response.data.result == 'fail') {
                    ElMessage.error(err.response.data.msg)
                } else {
                    ElMessage.error("未知错误")
                }
                return
            }
            console.log("YesterdayInfo - JSON.stringify(response) = "+JSON.stringify(response, null, 2))
            if(response.data.ok=='no')
            {
                ElMessage.error("获取昨日信息失败");
            }else if(response.data.ok=='yes'){
               this.signInNum=response.data.value.signInNum;
               this.newUsrNum=response.data.value.newUsrNum;
               this.newPostNum=response.data.value.newPostNum;
            }
        },
    }
}
</script>

<template>
    <el-container class="aside-upper-box">
          <el-container class="sub-box" style="background-color: rgb(246, 148, 148);margin-top:10px;">
                <span class="sub-text">昨日签到人数：{{ signInNum }} 人</span>
          </el-container>
          <el-container class="sub-box" style="background-color: rgb(156, 233, 49);margin-top:20px;">
                <span class="sub-text">昨日新增用户：{{ newUsrNum }} 个</span>
          </el-container>
          <el-container class="sub-box" style="background-color: rgb(148, 174, 246);margin-top:20px;">
                <span class="sub-text">昨日新增帖子：{{ newPostNum }} 个</span>
          </el-container>
    </el-container>
</template>

<style scoped>
/*整个组件的容器*/
.aside-upper-box{
    margin-top: 5vh;
    margin-left:1vw;
    position: relative;
    width:95%;
    height:30%;
    display: flex;
    flex-direction: column;
    /* background-color: white; */
}
.sub-box{
    position: relative;
    border-radius: 20px;
    height:1.5vh;
    width:100%;
}
.sub-text{
    margin-top:1.7vh;
    margin-left: 5vw;
    height:1.5vh;
    color:white; 
}
</style>

<!-- <script>
    export default {
    components:{
      
    },
    data() {
        return {
            reportedUsers: [
                { id: 1, name: 'RU 1' ,reason:'犯下了色欲之罪'},
                { id: 2, name: 'RU 2' ,reason:'犯下了傲慢之罪'},
                { id: 3, name: 'RU 3' ,reason:'犯下了贪婪之罪'},
                { id: 4, name: 'RU 4' ,reason:'犯下了暴怒之罪'},
                { id: 5, name: 'RU 5' ,reason:'犯下了懒惰之罪'},
                { id: 6, name: 'RU 6' ,reason:'犯下了暴食之罪'},
                { id: 7, name: 'RU 7' ,reason:'犯下了嫉妒之罪'},
            ],
        };
    },
    methods:{
        killUser(){

        },
    }
}
</script>

<template>
    <el-container class="aside-upper-box">
            <el-table :data="reportedUsers" border height="210" style="width: 100%;border-radius: 10px;">
                <el-table-column align="center" prop="id" label="Id" width="100" />
                <el-table-column prop="name" label="昵称" width="100" />
                <el-table-column fixed="right" label="操作">
                    <template #default="scope">
                        <el-popover
                            placement="bottom"
                            :width="200"
                            trigger="click"
                        >
                            <span>{{reportedUsers[scope.$index].reason}}</span>
                            <template #reference>
                            <el-button link type="primary" size="small">查看详情</el-button>
                            </template>
                        </el-popover>
                        <el-popconfirm
                            width="220"
                            confirm-button-text="确认"
                            cancel-button-text="取消"
                            :icon="InfoFilled"
                            icon-color="#626AEF"
                            title="确认封禁此用户？"
                        >
                            <template #reference>
                            <el-button link type="primary" size="small" @click="killUser">封禁用户</el-button>
                            </template>
                        </el-popconfirm>
                    </template>
                </el-table-column>
            </el-table>
    </el-container>
</template>

<style scoped>
/*整个组件的容器*/
.aside-upper-box{
    margin-top: 5vh;
    margin-left:1vw;
    border-radius: 10px;
    position: relative;
    width:95%;
    height:30%;
    background-color: white;
}
/*组件中每一条内容的容器*/
.list-box{
    width:100%;
    height:10vh;
    position: relative;
    background-color: rgb(228, 222, 222);
}
</style> -->
