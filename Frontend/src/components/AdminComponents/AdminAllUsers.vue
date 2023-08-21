<script>
    export default {
    components:{
      
    },
    data() {
        return {
            Users: [
                { id: 1, name: 'RU 1' ,regDate:"2023-8-1",collectedNum:8,credits:10,hostTeam:"阿根廷"},
                { id: 2, name: 'RU 2' ,regDate:"2023-8-1",collectedNum:9,credits:10,hostTeam:"阿根廷"},
                { id: 3, name: 'RU 3' ,regDate:"2023-8-1",collectedNum:3,credits:10,hostTeam:"阿根廷"},
                { id: 4, name: 'RU 4' ,regDate:"2023-8-1",collectedNum:4,credits:10,hostTeam:"阿根廷"},
                { id: 5, name: 'RU 5' ,regDate:"2023-8-1",collectedNum:0,credits:10,hostTeam:"阿根廷"},
                { id: 6, name: 'RU 6' ,regDate:"2023-8-1",collectedNum:6,credits:10,hostTeam:"阿根廷"},
                { id: 7, name: 'RU 7' ,regDate:"2023-8-1",collectedNum:7,credits:10,hostTeam:"阿根廷"},
            ],
        };
    },
    methods:{
      
    }
}
</script>

<template>
    <el-container class="main-lower-box">
        <el-table :data="Users" border height="300" style="width: 100%;border-radius: 10px;">
            <el-table-row label="111"/>
            <el-table-column align="center" prop="id" label="Id" width="100" />
            <el-table-column prop="name" label="昵称" width="150" />
            <el-table-column prop="regDate" label="注册时间" width="150" />
            <el-table-column align="center" prop="collectedNum" label="总收藏数" width="100" />
            <el-table-column align="center" prop="credits" label="积分" width="100" />
            <el-table-column prop="hostTeam" label="主队" width="150" />
            <el-table-column fixed="right" label="操作">
                <template #default="scope">
                    <el-popover
                        placement="bottom"
                        :width="200"
                        trigger="click"
                    >
                        <span>{{Users[scope.$index].reason}}</span>
                        <template #reference>
                        <el-button link type="primary" size="small">私信用户</el-button>
                        </template>
                    </el-popover>
                    <el-popconfirm
                        width="220"
                        confirm-button-text="确认"
                        cancel-button-text="取消"
                        :icon="InfoFilled"
                        icon-color="#626AEF"
                        title="确认删除此用户？"
                    >
                        <template #reference>
                        <el-button link type="primary" size="small">删除用户</el-button>
                        </template>
                    </el-popconfirm>
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