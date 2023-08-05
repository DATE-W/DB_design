<!-- 我的积分 v1.0 -->
<template>
    <div class="main-container">
        <div>
            <div class="user-title">{{ usertitle }}</div>
            <div class="user-point">
                {{ userpoint }}
                <span class="user-point-else">积分</span>
            </div>
            <div class="rectangle-container">
                <div v-for="(height, index) in rectangleHeights" :key="index" class="rectangle" :style="{
                    height: height + 'px',
                    marginTop: (rectangleHeights[maxHeightIndex] - height) + 'px',
                    backgroundColor: (index === userIndex) ? '#0BE4F7' : '#E2EFEE'
                }">
                    <div class="rectangle-point">{{ point[index] }}</div>
                    <div class="rectangle-title">{{ title[index] }}</div>
                </div>
            </div>
            <div class="point-detail">
                <span class="detail-title">积分明细:</span>
                <ul v-infinite-scroll="load" class="infinite-list" style="overflow: auto">
                    <li v-for="i in count" :key="i" class="infinite-list-item">{{ i }}</li>
                </ul>
            </div>
        </div>
        <div class="user-equity">
            <div class="equity-title">权益详情</div>
            <div>
                <el-collapse v-model="activeNames" @change="handleChange">
                    <el-collapse-item name="1">
                        <template #title>
                            <span style="display: inline-block; min-width: 450px;">平平无奇</span>
                        </template>
                        <div>
                            1
                        </div>
                    </el-collapse-item>
                    <el-collapse-item title="普通用户" name="2">
                        <div>
                            2
                        </div>
                    </el-collapse-item>
                    <el-collapse-item title="一贴成名" name="3">
                        <div>
                            3
                        </div>
                    </el-collapse-item>
                    <el-collapse-item title="球场金童" name="4">
                        <div>
                            4
                        </div>
                    </el-collapse-item>
                    <el-collapse-item title="明日之星" name="5">
                        <div>
                            5
                        </div>
                    </el-collapse-item>
                    <el-collapse-item title="名人堂" name="6">
                        <div>
                            6
                        </div>
                    </el-collapse-item>
                </el-collapse>
            </div>
        </div>
    </div>
</template>

<style scoped>
.user-title {
    font-family: YouYuan;
    font-weight: bold;
    font-size: 24px;
    margin-bottom: 10px;
}

.user-point {
    font-size: 20px;
}

.user-point-else {
    font-family: STZhongsong;
    font-size: 15px;
    margin-left: 5px;
}

.main-container {
    display: flex;
    align-items: flex-start;
}

.rectangle-container {
    display: flex;
    position: relative;
}

.rectangle {
    width: 80px;
    margin-right: 20px;
    border-radius: 8px;
    position: relative;
}

.rectangle-point {
    position: absolute;
    top: -20px;
    left: 0;
    width: 100%;
    text-align: center;
    font-size: 15px;
    color: #000;
}

.rectangle-title {
    position: absolute;
    bottom: -25px;
    left: 0;
    width: 100%;
    text-align: center;
    font-size: 15px;
    color: #000;
}

.user-equity {
    margin-left: 50px;
}

.equity-title {
    font-size: 18px;
    margin-bottom: 20px;
}

.point-detail {
    margin-top: 100px;
}

.detail-title {
    font-size: 18px;
}

.infinite-list {
    height: 300px;
    padding: 0;
    margin: 0;
    list-style: none;
}

.infinite-list .infinite-list-item {
    display: flex;
    align-items: center;
    justify-content: center;
    height: 50px;
    background: var(--el-color-primary-light-9);
    margin: 10px;
    color: var(--el-color-primary);
}

.infinite-list .infinite-list-item+.list-item {
    margin-top: 10px;
}
</style>

<script lang="ts">
import { ref } from 'vue'
export default {
    data() {
        return {
            usertitle: "普通用户",  //向后端请求获得用户的称号
            userpoint: 25,  //向后端请求获得用户的积分数
            rectangleHeights: [20, 40, 60, 90, 130, 190],  //矩形的长度
            title: ["平平无奇", "普通用户", "一贴成名", "球场金童", "明日之星", "名人堂"],  //称号名
            point: [0, 10, 50, 200, 500, 1000],  //各称号的最低积分
            userIndex: -1,   //判断用户是哪个称号范围
        };
    },
    mounted() {
        this.load();
        this.userIndex = this.findUserIndex;  //进入页面即显示用户的称号
    },
    computed: {
        maxHeightIndex() {
            // 找到最高矩形的高度在数组中的索引
            return this.rectangleHeights.indexOf(Math.max(...this.rectangleHeights));
        },
        findUserIndex() {
            for (let i = this.point.length - 1; i >= 0; i--) {
                if (this.userpoint >= this.point[i]) {
                    return i;
                }
            }
            return -1;
        }
    },
    setup() {
        const count = ref(0);
        const load = () => {
            count.value += 6
        };
        const activeNames = ref(['0']);
        const handleChange = (val: string[]) => {
            console.log(val);
        };
        return { count, activeNames, load, handleChange };
    }//折叠面板
};
</script>