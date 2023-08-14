<template>
    <div>
        <!-- 当前主题展示 -->
        <div style="display: flex; justify-content: space-between;">
            <div>当前主题</div>
            <div>
                <img :src="selectedTheme.url" alt="Selected Theme" v-if="selectedTheme" class="theme-circle" />
                <div v-else>没有选择主题</div>
            </div>
        </div>

        <el-divider></el-divider>

        <!-- 主题选项 -->
        <el-row class="frame-options">
            <el-col :span="8" v-for="(theme, index) in themeList" :key="index">
                <div class="frame-option">
                    <div class="theme-item" @click="showThemePreview(theme)">
                        <div class="theme-circle" :style="`background-image: url(${theme.url})`"
                            :class="{ 'selected-frame': selectedTheme === theme }"></div>
                        <div class="theme-name">{{ theme.name }}</div>
                    </div>
                </div>
            </el-col>
        </el-row>
    </div>
</template>
  
<script>
import { ElMessageBox } from 'element-plus';
export default {
    mounted() {

    },
    data() {
        return {
            themeList: [
                { name: '主题1', url: './src/assets/img/carousel1.png' },
                { name: '主题2', url: './src/assets/img/carousel2.png' },
                { name: '主题3', url: './src/assets/img/carousel1.png' },
                { name: '主题4', url: './src/assets/img/carousel2.png' },
                { name: '主题5', url: './src/assets/img/carousel1.png' },
                { name: '主题6', url: './src/assets/img/carousel2.png' },
                { name: '主题7', url: './src/assets/img/carousel1.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                { name: '主题8', url: './src/assets/img/carousel2.png' },
                // 添加更多主题对象...
            ],
            selectedTheme: { name: '当前主题', url: './src/assets/img/carousel1.png' }, // 初始化选中的主题
            previewImageUrl: './src/assets/img/carousel1.png' // 用于保存预览图片的 URL
        };
    },
    methods: {
        showThemePreview(theme) {
            this.previewImageUrl = theme.url;

            ElMessageBox({
                title: '切换主题',
                message: `
                    <div>
                        <p>是否切换到主题 ${theme.name}？</p>
                        <img src="${theme.url}" alt="Theme Preview" style="max-width: 100%;">
                    </div>
                `,
                showCancelButton: true,
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning',
                dangerouslyUseHTMLString: true // 允许使用 HTML 字符串
            }).then(() => {
                this.selectTheme(theme);
            }).catch(() => {
                this.previewImageUrl = '';
            });
        },
        selectTheme(theme) {
            this.selectedTheme = theme;
            // 执行选中主题相关操作
            this.previewImageUrl = '';
        }
    }
};
</script>
  
<style scoped>
.el-col {
    justify-content: center;
    display: flex;
}

/* 添加所需的样式规则 */
.frame-options {
    margin: 20px;
    text-align: center;
    overflow-y: auto;
    /* 添加垂直滚动条 */
    max-height: 740px;
    /* 设置最大高度，超过部分会出现滚动条 */
}

.frame-option {
    width: 100px;
    height: 150px;
    margin: 5px;
    cursor: pointer;
}

.selected-frame {
    outline: 5px solid #1890ff;
    /* 使用 outline 代替 border */
}

.theme-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    cursor: pointer;
}

.theme-circle {
    width: 100px;
    height: 100px;
    background-size: cover;
    border-radius: 50%;
}

.theme-name {
    margin-top: 5px;
    font-size: 12px;
}
</style>
