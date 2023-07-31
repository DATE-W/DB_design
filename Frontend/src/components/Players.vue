<!-- 
2152190_李凌朗_球员信息 v1.0.0
 v1.0.0 设计页面布局，与赛事界面统一左侧联赛选择方式，具体广告区、球队球员区跳转逻辑待实现，界面布局待优化
 v1.0.1 重构页面布局，添加图片对布局进行完善
-->
<template>
  <my-nav></my-nav>

  <!-- 左侧联赛选择器 -->
  <div class="left" style="left:5vw">

    <div class="league" style="top:1vw">
      <p class="textTypeLeague" style="left:2.5vw">全部赛事</p>
    </div>

    <div class="league" style="top:5.8vw">
      <img src="../assets/img/pmlogo.png" class="imgLogo">
      <p class="textTypeLeague">英超</p>
    </div>

    <div class="league" style="top:10.6vw">
      <img src="../assets/img/lllogo.png" class="imgLogo">
      <p class="textTypeLeague">西甲</p>
    </div>

    <div class="league" style="top:15.4vw">
      <img src="../assets/img/salogo.png" class="imgLogo">
      <p class="textTypeLeague">意甲</p>
    </div>

    <div class="league" style="top:20.2vw;">
      <img src="../assets/img/bllogo.png" class="imgLogo" style="border-radius: 0vw; ">
      <p class="textTypeLeague">德甲</p>
    </div>

    <div class="league" style="top:25vw">
      <img src="../assets/img/le1logo.png" class="imgLogo">
      <p class="textTypeLeague">法甲</p>
    </div>

    <div class="league" style="top:30vw">
      <img src="../assets/img/cslogo.png" class="imgLogo" style="left:1.5vw;top:0.2vw">
      <p class="textTypeLeague">中超</p>
    </div>

    <div class="league" style="top:35vw">
      <p class="textTypeLeague" style="left:2.5vw">其他赛事</p>
    </div>
  </div>

  <!-- 高光球员合集 -->
  <div class="highlightPlayer">
    <p>高光球员合集</p>
    <img src="../assets/img/highlightPlayer.png" class="highlightPlayerPic" alt="Transfer to highlight player page" @click="toHighlightPlayer">
  </div>

  <el-main>
    <div class="search-container">
      <el-icon class="search-icon">
        <Search />
      </el-icon>
      <el-input class="search-input" v-model="searchKeyword" placeholder="请输入关键词"
        @keyup.enter.native="handleSearch"></el-input>
    </div>
    <div class="page-container">
      <el-pagination @current-change="handlePageChange" :current-page="currentPage" :page-size="pageSize"
        layout="prev, pager, next, jumper" :total="totalItems"></el-pagination>
    </div>
  </el-main>



  <!-- 广告 -->
  <div class="AD" style="right:5vw;">
    <img src="../assets/img/AD.png" class="ADPic" alt="This is an AD image" >
  </div>
</template>

<script>
import MyNav from './nav.vue';

export default {
  components: {
    'my-nav': MyNav
  },
  methods:{
      toHighlightPlayer(){
        this.$router.push('/highlightPlayerDetail');
      },
    }
}
</script>

<style scoped>

/* 左侧容器 */
.left {
  position: absolute;
  width: 15vw;
  height: 40vw;
  flex-shrink: 0;
  /* 正式版本 */
  background: rgb(240, 240, 240);
  /* 测试版本 */
  /* background: #4BDFBC;  */
}

/* 广告 */
.AD {
  position: absolute;
  width: 17vw;
  height: 40vw;
  flex-shrink: 0;
  bottom: 2vw;
  background: #4B8FDF;
}

/* 联赛选择按钮 */
.league {
  position: absolute;
  width: 13vw;
  height: 4vw;
  flex-shrink: 0;
  border-radius: 2vw;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);
  left: 1vw;
  background-color: #ffffff;
  transition: background-color 0.8s ease;
}

.league:hover {
  background-color: rgb(246, 77, 77);
}

/* 高光球员合集 */
.highlightPlayer {
  position: absolute;
  width: 50vw;
  height: 7.5vw;
  top: 5vw;
  left: 25vw;
  border: 0.1vw solid var(--colors-light-eaeaea-100, #d1d1d1);
  background: #83dfec;
}

/* 搜索框 */
.search-container {
    margin-top: 7vw;
    margin-left: 19vw;
    display: flex;
    align-items: center;
}

.search-input {
    width: 30%;
}

.search-icon {
    font-size: 24px;
}

/* 字体样式 */

/* 左侧联赛名称字体样式 */
.textTypeLeague {
  position: absolute;
  width: 8vw;
  height: 2vw;
  top: -1.3vw;
  left: 6vw;
  color: var(--colors-text-dark-172239100, #172239);
  font-feature-settings: 'clig' off, 'liga' off;
  font-family: Verdana;
  font-size: 2vw;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* 图片样式 */

/* 联赛图标 */
.imgLogo {
  position: absolute;
  width: 4vw;
  height: 4vw;
  border-radius: 2vw;
  left: 1vw;
  object-fit: cover;
}

/* 广告图片 */
.ADPic{
  position: absolute;
  width: 17vw;
  height: 40vw;
  flex-shrink: 0;
  bottom: 0vw;
}

/* 高光球员跳转图片 */
.highlightPlayerPic {
  position: absolute;
  width: 50vw;
  height: 7.5vw;
  top: 0vw;
  left: 0vw;
}



/* 各个球员图片 */
.playerPic1 {
  background: rgb(220, 138, 241);
}
</style>
