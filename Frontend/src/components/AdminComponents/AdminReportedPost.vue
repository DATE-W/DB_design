<script>
    export default {
    components:{
      
    },
    data() {
        return {
            reportedPost: [
                { id: 1, tittle: 'Post 1' ,postUser:"U 1",reportUser:"RU 1",reason:'犯下了色欲之罪'},
                { id: 2, tittle: 'Post 2' ,postUser:"U 2",reportUser:"RU 2",reason:'犯下了傲慢之罪'},
                { id: 3, tittle: 'Post 3' ,postUser:"U 3",reportUser:"RU 3",reason:'犯下了贪婪之罪'},
                { id: 4, tittle: 'Post 4' ,postUser:"U 4",reportUser:"RU 4",reason:'犯下了暴怒之罪'},
                { id: 5, tittle: 'Post 5' ,postUser:"U 5",reportUser:"RU 5",reason:'犯下了懒惰之罪'},
                { id: 6, tittle: 'Post 6' ,postUser:"U 6",reportUser:"RU 6",reason:'犯下了暴食之罪'},
                { id: 7, tittle: 'Post 7' ,postUser:"U 7",reportUser:"RU 7",reason:'犯下了嫉妒之罪'},
            ],
        };
    },
    methods:{
        killPost(){

        },
        toPost(){
            this.$router.push({
                path: '/detail',
                query: { fromAdmin: 1 }
            });
        },
    }
}
</script>

<template>
    <el-container class="main-upper-box">
        <el-table :data="reportedPost" border height="300" style="width: 100%;border-radius: 10px;">
            <el-table-column align="center" prop="id" label="Id" width="100" />
            <el-table-column prop="tittle" label="标题" width="150" />
            <el-table-column prop="postUser" label="发帖人" width="100" />
            <el-table-column prop="reportUser" label="举报人" width="100" />
            <el-table-column prop="reason" label="举报理由" width="200" />
            <el-table-column fixed="right" label="操作">
                <template #default>
                    <el-popover
                        placement="bottom"
                        :width="200"
                        trigger="click"
                    >
                        <template #reference>
                        <el-button link type="primary" size="small" @click="toPost">查看帖子</el-button>
                        </template>
                    </el-popover>
                    <el-popconfirm
                        width="200"
                        confirm-button-text="确认"
                        cancel-button-text="取消"
                        :icon="InfoFilled"
                        icon-color="#626AEF"
                        title="确认删除此帖？"
                    >
                        <template #reference>
                        <el-button link type="primary" size="small" @click="killPost">删除帖子</el-button>
                        </template>
                    </el-popconfirm>
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
</style>