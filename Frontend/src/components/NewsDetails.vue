<template>
    <div>
        <!-- 顶部导航栏 -->
        <my-nav></my-nav>
        <div class="mainBox">
            <!-- 导航及简介 -->
            <div>
                <!-- 返回导航 -->
                <el-page-header @back="goBack()" :icon="ArrowLeft">
                    <template #content>
                        <div class="flex items-center">
                            <el-avatar :size="32" class="mr-3" :src="returnLogo(Details.newsBody.matchTag)" />
                            <span style="position: relative;left:1vw;top:-0.5vh"> <b>{{ Details.newsBody.title }}</b>
                            </span>
                        </div>
                    </template>
                    <el-descriptions :column="2" size="small" class="mt-4">
                        <el-descriptions-item label="News_ID：" :label-style="{ fontWeight: 300 }"><b>{{
                            Details.newsBody.news_id }}</b></el-descriptions-item>
                        <el-descriptions-item label="PublishDateTime："><b>{{ returnTime(Details.newsBody.publishDateTime)
                        }}</b></el-descriptions-item>
                        <el-descriptions-item label="Place："><b>{{ returnPlace(Details.newsBody.matchTag)
                        }}</b></el-descriptions-item>
                        <el-descriptions-item label="Tags_News：">
                            <el-tag class="ml-2" type="warning"><b>{{ Details.newsBody.matchTag }}&nbsp;</b></el-tag>
                            <el-tag class=" ml-2" type="success" style="margin-left: 5px;"><b>
                                    &nbsp;{{ Details.newsBody.propertyTag }}&nbsp;
                                </b></el-tag>
                        </el-descriptions-item>
                    </el-descriptions>
                </el-page-header>
            </div>
            <!-- 新闻正文 -->
            <div>
                <!-- 标题 -->
                <div class="title">
                    {{ Details.newsBody.title }}
                </div>
                <!-- 正文 -->
                <div class="content">
                    {{ Details.newsBody.contains }}
                </div>
                <!-- 图片 -->
                <div>
                    <el-col :span="6" v-for="item in  Details.pictureRoutes">
                        <img v-if="item != null && matchMP4(item) == false" referrerPolicy='no-referrer' :src="item"
                            alt="Image" class="imgItem">
                        <video v-if="item != null && matchMP4(item) == true" referrerPolicy='no-referrer' ref="videoPlayer"
                            :src="item.pictureRoutes[0]" class="imgItem" controls />
                    </el-col>
                </div>
            </div>
        </div>

    </div>
    <el-backtop :right="100" :bottom="100" />
</template>
  
<script>
import MyNav from './nav.vue';
import ChinaLogo from '../assets/img/cslogo.png';
import EnglandLogo from '../assets/img/pmlogo.png';
import SpainLogo from '../assets/img/lllogo.png';
import GermanyLogo from '../assets/img/bllogo.png';
import ItalyLogo from '../assets/img/salogo.png';
import FranceLogo from '../assets/img/le1logo.png';

export default {
    components: {
        'my-nav': MyNav
    },

    data() {
        return {
            Details: {
                newsBody: {
                    matchTag: "",
                    propertyTag: "",
                    title: "",
                    summary: "",
                    contains: "",
                    news_id: 0,
                    publishDateTime: '',
                },
                pictureRoutes: [],
            },
        };
    },
    created() {
        const queryString = this.$route.query.data;
        if (queryString) {
            const decodedString = decodeURIComponent(queryString);
            this.Details = JSON.parse(decodedString);
        }
    },

    methods: {
        returnLogo(tag) {
            if (tag == '中超')
                return ChinaLogo;
            else if (tag == '英超')
                return EnglandLogo;
            else if (tag == '西甲')
                return SpainLogo;
            else if (tag == '德甲')
                return GermanyLogo;
            else if (tag == '意甲')
                return ItalyLogo;
            else if (tag == '法甲')
                return FranceLogo;
        },
        returnTime(time) {
            return time.replace(/T/g, '   ');
        },
        returnPlace(tag) {
            if (tag == '中超')
                return 'China';
            else if (tag == '英超')
                return 'England';
            else if (tag == '西甲')
                return 'Spain';
            else if (tag == '德甲')
                return 'Germany';
            else if (tag == '意甲')
                return 'Italy';
            else if (tag == '法甲')
                return 'France';
        },
        //匹配mp4字符，用于判断视频还是图片的渲染
        matchMP4(str) {
            return str.includes('mp4');
        },
        goBack() {
            this.$router.back();
        }
    },
};
</script>

<style scoped>
.mainBox {
    width: 60vw;
    position: relative;
    left: 20vw;
    top: 2vh;
}

.title {
    position: relative;
    top: 2vh;
    left: 0;
    flex-shrink: 0;
    color: #000;
    font-family: FZYaoTi;
    font-size: 30px;
    font-style: normal;
    font-weight: 700;
    line-height: normal;
}

.content {
    position: relative;
    top: 4vh;
    font-family: FZSongTi;
    font-size: 18px;
    font-style: normal;
    font-weight: 500;
    line-height: 30px;
    /* line-height: 300%; */
}

.imgItem {
    position: relative;
    top: 6vh;
    left: 0;
    border-radius: 6px;
    width: 50vw;
}
</style>