<template>
    <div class="common-layout">
        <el-container>
            <el-header :class="['forum-header', selectedColor]">
                {{ selectedTopic }}
                <br>
                {{ selectedDescription }}
            </el-header>
            <!-- 上端论坛话题显示 -->
            <el-container>
                <el-aside width="200px">
                    <div class="button-container">
                        <el-button class="button-item" round>点此发帖</el-button>
                        <div v-for="row in buttonRows" :key="row">
                            <el-button v-for="button in row" :key="button.type" @click="selectTopic(button)" class="button-item" text>
                                {{ button.text }}
                            </el-button>
                        </div>
                    </div>
                </el-aside>
                <!-- 左侧选择话题 -->
                <el-main>Main</el-main>
                <!-- 右侧展示当前话题的帖子 -->
            </el-container>
        </el-container>
    </div>
</template>

<style scoped>
.forum-header {
    text-align: center;
}

.button-container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin-top: 20%;
}

/* 每个按钮之间有间隔 */
.button-item {
    margin: 20px;
}

.LL {
    background-color: #e0f1ff;
}

.PL {
    background-color: #ffe6e6;
}

.BL {
    background-color: #e6f2ff;
}

.SA {
    background-color: #ffe6ff;
}

.L1 {
    background-color: #e6ffe6;
}

.CSL {
    background-color: #f00e0e;
}
</style>
  
<script>
import { defineComponent } from 'vue';

export default defineComponent({
    data() {
        return {
            selectedTopic: '论坛',
            selectedColor: '', 
            selectedDescription: '',
            buttons: [
                { type: 'LL', text: '西甲', color: 'LL', description: '西甲联赛是西班牙的顶级足球联赛'},
                { type: 'PL', text: '英超', color: 'PL', description: '英超联赛是英格兰的顶级足球联赛'},
                { type: 'BL', text: '德甲', color: 'BL', description: '德甲联赛是德国的顶级足球联赛'},
                { type: 'SA', text: '意甲', color: 'SA', description: '意甲联赛是意大利的顶级足球联赛'},
                { type: 'L1', text: '法甲', color: 'L1', description: '法甲联赛是法国的顶级足球联赛'},
                { type: 'CSL', text: '中超', color: 'CSL', description: '中超联赛是中国建立的足球联赛，拥有武磊等顶级球员'},
            ],
        };
    },
    computed: {
        buttonRows() {
            const rows = [];
            const buttonsPerRow = 1;
            for (let i = 0; i < this.buttons.length; i += buttonsPerRow) {
                const row = this.buttons.slice(i, i + buttonsPerRow);
                rows.push(row);
            }
            return rows;
        },
    },
    methods: {
        selectTopic(button) {
            this.selectedTopic = button.text;
            this.selectedColor = button.color;
            this.selectedDescription = button.description;
        },
    },
});
</script>
