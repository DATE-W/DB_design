<template>
    <div>
        <!-- 当前球队展示 -->
        <div style="display: flex; justify-content: space-between;">
            <div>当前球队</div>
            <div>
                <img :src="selectedteam.url" alt="Selected team" v-if="selectedteam" class="team-circle" />
                <div v-else>没有选择球队</div>
            </div>
        </div>

        <el-divider></el-divider>

        <!-- 球队选项 -->
        <el-row class="frame-options">
            <el-col :span="8" v-for="(team, index) in teamList" :key="index">
                <div class="frame-option">
                    <div class="team-item" @click="showteamPreview(team)">
                        <div class="team-circle" :style="`background-image: url(${team.url})`"
                            :class="{ 'selected-frame': selectedteam === team }"></div>
                        <div class="team-name">{{ team.name }}</div>
                    </div>
                </div>
            </el-col>
        </el-row>
    </div>
</template>
  
<script>
import { ElMessage, ElMessageBox } from 'element-plus';
import axios from 'axios';
export default {
    mounted() {

    },
    data() {
        return {
            teamList: [
                { name: '球队1', url: './src/assets/img/carousel1.png' },
                { name: '球队2', url: './src/assets/img/carousel2.png' },
                { name: '球队3', url: './src/assets/img/carousel1.png' },
                { name: '球队4', url: './src/assets/img/carousel2.png' },
                { name: '球队5', url: './src/assets/img/carousel1.png' },
                { name: '球队6', url: './src/assets/img/carousel2.png' },
                { name: '球队7', url: './src/assets/img/carousel1.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                { name: '球队8', url: './src/assets/img/carousel2.png' },
                // 添加更多球队对象...
            ],
            selectedteam: { name: '当前球队', url: './src/assets/img/carousel1.png' }, // 初始化选中的球队
            previewImageUrl: './src/assets/img/carousel1.png' // 用于保存预览图片的 URL
        };
    },
    methods: {
        showteamPreview(team) {
            this.previewImageUrl = team.url;

            ElMessageBox({
                title: '切换球队',
                message: `
                    <div>
                        <p>是否切换到球队 ${team.name}？</p>
                        <img src="${team.url}" alt="team Preview" style="max-width: 100%;">
                    </div>
                `,
                showCancelButton: true,
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning',
                dangerouslyUseHTMLString: true // 允许使用 HTML 字符串
            }).then(() => {
                this.selectteam(team);
            }).catch(() => {
                this.previewImageUrl = '';
            });
        },
        async selectteam(team) {
            this.selectedteam = team;
            // 执行选中球队相关操作
            this.previewImageUrl = '';
            const teamname = this.selectedteam.name;
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                    Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/User/modifyteam', { teamname }, { headers })
            } catch (err) {
                console.log(err)
                ElMessage({
                    message: '未知错误',
                    grouping: false,
                    type: 'error',
                })
                return
            }
            console.log(response);
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
    max-height: 70vh;
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

.team-item {
    display: flex;
    flex-direction: column;
    align-items: center;
    cursor: pointer;
}

.team-circle {
    width: 100px;
    height: 100px;
    background-size: cover;
    border-radius: 50%;
}

.team-name {
    margin-top: 5px;
    font-size: 12px;
}
</style>
