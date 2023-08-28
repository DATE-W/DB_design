<script>
import axios from 'axios';
import { ElMessage, ElMessageBox } from 'element-plus';
export default {
    components:{
        
    },
    mounted(){
        this.getReportedPost();
    },
    data() {
        return {
            reportedPost: [],
            tittle:"",
            contains:"",
        };
    },
    methods:{
        async killPost(index){
            console.log("killPost")
            let response
            try {
                response = await axios.post('api/report/agreeReport',  {
                    reporter_id: this.reportedPost[index].reporter_id,
                    post_id: this.reportedPost[index].post_id,
                    reply: " ",
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
            console.log("response.data.ok = " + response.data.ok);
            if (response.data.ok == 'yes') {
                ElMessage.success("成功删除本帖");
                location.reload();
            }
            else {
               ElMessage.error("未找到相关举报信息");
            }
        },
        async toPost(postId){
            console.log("getPost")
            let response
            try {
                response = await axios.post('/api/Post/getPostInfo',  {
                    post_id: postId,
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
            console.log("getPostInfo - JSON.stringify(response) = "+JSON.stringify(response, null, 2))
            if (response.data.ok == 'yes') {
               this.tittle=response.data.value.title;
               this.contains=response.data.value.contains;
            }
            else {
               ElMessage.error("获取帖子信息失败");
            }
            ElMessageBox.alert(this.contains, this.tittle, {
                confirmButtonText: 'OK',
            })
        },
        async getReportedPost(){
            console.log("start get reported post")
            let response
            try {
                response = await axios.get('api/report/getReportInfo');
            } catch (err) {
                console.error(err);
                if (err.response.data.result == 'fail') {
                    ElMessage.error(err.response.data.msg)
                } else {
                    ElMessage.error("未知错误")
                }
                return
            }
            console.log("ReportedPost - JSON.stringify(response) = "+JSON.stringify(response, null, 2))
            if(response.data.ok=='no')
            {
                ElMessage.error("获取被举报帖子列表失败");
            }else if(response.data.ok=='yes'){
               // 遍历传来的数据并进行转换
                response.data.value.forEach(item => {
                    const convertedItem = {
                        post_id: item.post_id,
                        tittle: item.title,
                        publisherName: item.publisherName,
                        reporterName: item.reporterName,
                        reporter_id:item.reporter_id,
                        description: item.description || "没有提供原因"
                    };
                // 将转换后的数据添加到 reportedPost 数组中
                this.reportedPost.push(convertedItem);
                this.reportedPost.sort((a, b) => a.post_id - b.post_id);
                });
            }
        },
        async cancelReport(index){
            console.log("cancelReport")
            console.log("index " + index);
            let response
            try {
                response = await axios.post('api/report/cancelReport',  {
                    reporter_id: this.reportedPost[index].reporter_id,
                    post_id: this.reportedPost[index].post_id,
                    reply: " ",
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
                ElMessage.success("取消举报成功");
                location.reload();
            }
            else {
               ElMessage.error("未找到相关举报信息");
            }
        },
    }
}
</script>

<template>
    <el-container class="main-upper-box">
        <el-table :data="reportedPost" border height="300" style="width: 100%;border-radius: 10px;">
            <el-table-column align="center" prop="post_id" label="帖子Id" width="100" />
            <el-table-column prop="tittle" label="帖子标题" width="150" />
            <el-table-column prop="publisherName" label="发帖人" width="100" />
            <el-table-column prop="reporterName" label="举报人" width="100" />
            <el-table-column prop="description" label="举报理由" width="200" />
            <el-table-column fixed="right" label="操作">
                <template #default="scope">
                    <el-button link type="primary" size="small" @click="toPost(reportedPost[scope.$index].post_id)">查看帖子</el-button>
                    <el-button link type="primary" size="small" @click="killPost(scope.$index)">删除帖子</el-button>
                    <el-button link type="primary" size="small" @click="cancelReport(scope.$index)">取消举报</el-button>
                </template>
            </el-table-column>
        </el-table>
    </el-container>
</template>

<style scoped>
.main-upper-box{
    margin-top: 2.3vh;
    margin-right:1vw;
    border-radius: 10px;
    position: relative;
    width:98%;
    height:46%;
    background-color: white;
}
.cancelReport{
    cursor: pointer;
    margin-left: 40px;
}
.cancelReport:hover{
    color:red;
}
</style>