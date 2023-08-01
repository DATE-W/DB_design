<template>
    <div class="common-layout">
        <el-container>
            <el-header>
                <el-page-header @back="goBack">
                    <template #content>
                        <span class="text-large font-600 mr-3"> 资料编辑 </span>
                    </template>
                </el-page-header>
            </el-header>
            <el-main>
                <el-tabs type="border-card" tab-position="left" style="height: 80vh;" class="demo-tabs">
                    <el-tab-pane label="个人资料">
                        昵称
                        <el-input class="inputbox"></el-input>
                        个人账号
                        <el-input class="inputbox"></el-input>

                    </el-tab-pane>
                    <el-tab-pane label="网页主题">网页主题</el-tab-pane>
                    <el-tab-pane label="还没想好">Role</el-tab-pane>
                    <el-tab-pane label="还没想好">Task</el-tab-pane>
                </el-tabs>
            </el-main>
        </el-container>
    </div>
</template>
  
<script>
export default {
    data() {
        return {

        };
    },
    methods: {
        goBack() {
            this.$router.push('/personal')
        }

    },
};
</script>
<style scoped>
.el-main {
    padding: 0px;
}

.titleLayout {
    position: absolute;
    display: flex;
    width: 940px;
    flex-direction: column;
    flex-shrink: 0;
}

.el-tabs--right .el-tabs__content,
.el-tabs--left .el-tabs__content {
    height: 100%;
}

.titleh1Typo {
    color: var(--colors-text-dark-172239100, #172239);
    font-family: Verdana;
    font-size: 40px;
    font-style: normal;
    font-weight: 600;
    line-height: 48px;
}

.inputbox {
    display: flex;
    width: 40vw;
}
</style>