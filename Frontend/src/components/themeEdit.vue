<template>
    <div>
        <div class="avatar-container">
            <div class="avatar-frame" :style="`background-color: ${selectedFrameColor}`">
                <el-avatar :src="avatarUrl" class="avatar"></el-avatar>
            </div>
        </div>
        <el-divider></el-divider>
        <el-row class="frame-options">
            <el-col :span="6" v-for="(frameColor, index) in frameColors" :key="index">
                <div class="frame-option" :class="{ 'selected-frame': selectedFrameColor === frameColor }"
                    :style="`background-color: ${frameColor}`" @click="selectFrame(frameColor)"></div>
            </el-col>
        </el-row>
    </div>
</template>
  
<script>
import { ref } from 'vue';

export default {
    setup() {
        const avatarUrl = ref('./src/assets/img/carousel1.png'); // 头像图片url
        const selectedFrameColor = ref('#2196F3'); // 默认选中的头像框颜色

        const frameColors = [
            '#2196F3',
            '#4CAF50',
            '#FF5722',
            '#FF9800',
            '#E91E63',
            '#9C27B0',
            '#607D8B',
        ]; // 头像框颜色选项

        const selectFrame = (frameColor) => {
            selectedFrameColor.value = frameColor; // 更新选中的头像框颜色
        };

        return {
            avatarUrl,
            selectedFrameColor,
            frameColors,
            selectFrame,
        };
    },
};
</script>
  
<style scoped>
.main-container {
    height: 100vh;
    padding: 0 20vw;
}

.page-title {
    font-size: 24px;
    font-weight: bold;
    color: #333;
}

.avatar-container {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 400px;
}

.avatar-frame {
    display: inline-block;
    padding: 5px;
    border-radius: 50%;
    border: 2px solid #fff;
}

.avatar {
    width: 200px;
    height: 200px;
}

.frame-options {
    margin-top: 30px;
}

.frame-option {
    width: 60px;
    height: 60px;
    border-radius: 50%;
    cursor: pointer;
}

.selected-frame {
    border: 3px solid #000;
}
</style>
  