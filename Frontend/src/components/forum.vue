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
<<<<<<< Updated upstream
                        <el-button class="button-item" round>点此发帖</el-button>
                        <div v-for="row in buttonRows" :key="row">
                            <el-button v-for="button in row" :key="button.type" @click="selectTopic(button)" class="button-item" text>
                                {{ button.text }}
=======
                        <el-button class="postButton" round>
                            <text class="postButtonText" @click="redirectToEditPost">点此发帖</text>
                        </el-button>
                        <div v-for="row in buttonRows" :key="row">
                            <el-button v-for="button in row" :key="button.type" @click="selectTopic(button)"
                                class="raceButtonBox" text>
                                <div :class="['raceButtonCircle', button.color]"></div>
                                <div class="raceButtonText">{{ button.text }}</div>
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
.forum-header {
    text-align: center;
=======
.bkBox {
    position: absolute;
    width: 99.5%;
    height: 98.9%;
    background-color: #F5F7FA;
}

.forum-header {
    text-align: center;
    height: 5.2vw;
>>>>>>> Stashed changes
}

.button-container {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    margin-top: 20%;
}

<<<<<<< Updated upstream
/* 每个按钮之间有间隔 */
.button-item {
    margin: 20px;
}

=======
/*次顶部显示style*/
.subHeaderBox {
    text-align: center;
    height: 5.2vw;
    background-color: #93eefc;
}

.subTittle {
    color: black;
    font-family: NSimSun;
    font-size: 1.8rem;
    font-style: normal;
    font-weight: 600;
    line-height: normal;
    height: 0.7vw;
}

.subDescription {
    color: black;
    font-family: STKaiti;
    font-size: 1.5rem;
    font-style: normal;
    font-weight: 400;
    line-height: normal;
    height: 0.7vw;
}

/* 发帖按钮 */
.postButton {
    margin-top: 1vw;
    margin-bottom: 0.3vw;
    border-radius: 8vw;
    background-color: #3e28e3;
    box-shadow: 1px 1px 5px 0px rgba(0, 0, 0, 0.25);
    width: 14vw;
    height: 2.5vw;
}

.postButtonText {
    color: rgb(255, 255, 255);
    font-family: Ubuntu;
    font-size: 1.2rem;
    font-style: normal;
    font-weight: 700;
    line-height: normal;
    letter-spacing: 0.8px;
}

/*各联赛按钮*/
.raceButtonBox {
    margin: 0.5vw;
    border-radius: 20vw;
    height: 1.5vw;
}

.raceButtonCircle {
    position: relative;
    left: -1.5vw;
    border-radius: 50%;
    width: 1.7vw;
    height: 1.7vw;
}

.raceButtonText {
    font-family: Ubuntu;
    font-size: 1rem;
    font-style: normal;
    font-weight: 600;
    line-height: normal;
    letter-spacing: 0.8px;
}

/*每个联赛的横幅颜色*/
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
    components: {
        'my-nav': MyNav
    },
>>>>>>> Stashed changes
    data() {
        return {
            selectedTopic: '论坛',
            selectedColor: '',
            selectedDescription: '',
<<<<<<< Updated upstream
=======
            isButtonClicked: false,
>>>>>>> Stashed changes
            buttons: [
                { type: 'LL', text: '西甲', color: 'LL', description: '西甲联赛是西班牙的顶级足球联赛' },
                { type: 'PL', text: '英超', color: 'PL', description: '英超联赛是英格兰的顶级足球联赛' },
                { type: 'BL', text: '德甲', color: 'BL', description: '德甲联赛是德国的顶级足球联赛' },
                { type: 'SA', text: '意甲', color: 'SA', description: '意甲联赛是意大利的顶级足球联赛' },
                { type: 'L1', text: '法甲', color: 'L1', description: '法甲联赛是法国的顶级足球联赛' },
                { type: 'CSL', text: '中超', color: 'CSL', description: '中超联赛是中国建立的足球联赛，拥有武磊等顶级球员' },
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
        redirectToEditPost() {
            //跳转到论坛页面的逻辑
            this.$router.push('/EditPost')
        }
    },
});
</script>
