<script>
    export default {
    components:{
      
    },
    data() {
        return {
           
        };
    },
    methods:{
      
    }
}
</script>

<template>
    <el-container class="nav-header">
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
    </el-container>
</template>

<style scoped>
/*管理员界面头部导航栏*/
.nav-header {
    background-color: white;
    position: relative;
    height: 8vh;
    width: 99%;
    border-radius: 10px;
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
</style>