<!-- 
2152190_李凌朗_球员信息 v1.0.0
 v1.0.0 设计页面布局，与赛事界面统一左侧联赛选择方式，具体广告区、球队球员区跳转逻辑待实现，界面布局待优化
-->
<template>
  <my-nav></my-nav>

  <!-- 左侧联赛选择器 -->
  <div class="left" style="left:5rem">

    <div class="league" style="top:1rem">
      <p class="textTypeLeague" style="left:2.5rem">全部赛事</p>
    </div>

    <div class="league" style="top:5.8rem">
      <img src="../assets/img/pmlogo.png" class="imgLogo">
      <p class="textTypeLeague">英超</p>
    </div>

    <div class="league" style="top:10.6rem">
      <img src="../assets/img/lllogo.png" class="imgLogo">
      <p class="textTypeLeague">西甲</p>
    </div>

    <div class="league" style="top:15.4rem">
      <img src="../assets/img/salogo.png" class="imgLogo">
      <p class="textTypeLeague">意甲</p>
    </div>

    <div class="league" style="top:20.2rem;">
      <img src="../assets/img/bllogo.png" class="imgLogo" style="border-radius: 0rem; ">
      <p class="textTypeLeague">德甲</p>
    </div>

    <div class="league" style="top:25rem">
      <img src="../assets/img/le1logo.png" class="imgLogo">
      <p class="textTypeLeague">法甲</p>
    </div>

    <div class="league" style="top:30rem">
      <img src="../assets/img/cslogo.png" class="imgLogo" style="left:1.5rem;top:0.2rem">
      <p class="textTypeLeague">中超</p>
    </div>

    <div class="league" style="top:35rem">
      <p class="textTypeLeague" style="left:2.5rem">其他赛事</p>
    </div>
  </div>

  <!-- 高光球员合集 -->
  <div class="highlightPlayer">
    <p>高光球员合集</p>
  </div>

  <!-- 队伍名称1 -->
  <div class="teamName1">
    <p>球队1</p>
  </div>

  <!-- 球员照片1 -->
  <div class="playerPicture1">
    <p>球员图片</p>
  </div>

  <!-- 队伍名称2 -->
  <div class="teamName2">
    <p>球队2</p>
  </div>

  <!-- 球员照片2 -->
  <div class="playerPicture2">
    <p>球员图片</p>
  </div>




  <!-- 广告 -->
  <div class="AD" style="right:5rem;">
    <p>广告</p>
  </div>
</template>

<script>
import MyNav from './nav.vue';

export default {
  components: {
    'my-nav': MyNav
  }
}
</script>



<style scoped>

/* 框体样式 */

/* 左侧容器 */
.left
{
  position:absolute;
  width: 15rem;
  height: 40rem;
  flex-shrink: 0; 
  /* 正式版本 */
  background: rgb(240, 240, 240);
  /* 测试版本 */
  /* background: #4BDFBC;  */
}

/* 广告 */
.AD
{
  position:absolute;
  width: 17rem;
  height: 40rem;
  flex-shrink: 0;
  bottom: 2rem;
  /* 正式版本 */
  
  /* 测试版本 */
  background: #4B8FDF; 
}

/* 联赛选择按钮 */
.league
{
  position:absolute;
  width: 13rem;
  height: 4rem;
  flex-shrink: 0; 
  border-radius: 2rem;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA); 
  left:1rem;
  background-color: #ffffff;
  transition:background-color 0.8s ease;
}
.league:hover
{
  background-color: rgb(246, 77, 77);
}

/* 高光球员合集 */
.highlightPlayer
{
  position:absolute;
  width: 50rem;
  height: 7.5rem;
  top: 4.5rem;
  left: 20rem;
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1); 

  /* 测试版本，正式版本删去 */
  background: #83dfec;
}

/* 队伍名称1 */
.teamName1
{
  position:absolute;
  border-radius: 1rem;
  width: 8rem;
  height: 3rem;
  top: 15rem;
  left: 22rem;
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1); 

  /* 测试版本，正式版本删去 */
  background: rgb(144, 245, 119);
}

/* 队伍名称2 */
.teamName2
{
  position:absolute;
  border-radius: 1rem;
  width: 8rem;
  height: 3rem;
  top: 30rem;
  left: 22rem;
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1); 

  /* 测试版本，正式版本删去 */
  background: rgb(144, 245, 119);
}


/* 字体样式 */

/* 左侧联赛名称字体样式 */
.textTypeLeague
{
  position:absolute;
  width: 8rem;
  height: 2rem;
  top: -1.3rem;
  left: 6rem;
  color: var(--colors-text-dark-172239100, #172239);
  font-feature-settings: 'clig' off, 'liga' off;
  font-family: Verdana;
  font-size: 2rem;
  font-style: normal;
  font-weight: 600;
  line-height: normal; 
}

/* 图片样式 */

/* 联赛图标 */
.imgLogo
{
  position: absolute;
  width: 4rem;
  height: 4rem;
  border-radius: 2rem; 
  left: 1rem;
  object-fit: cover;
}

/* 各个球员图片 */
.playerPicture1
{
  position:absolute;
  width: 50rem;
  height: 8rem;
  top: 20rem;
  left: 20rem;

  flex-shrink: 0; 
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1); 
  /* 正式版本 */
  background: rgb(220, 138, 241);
}

.playerPicture2
{
  position:absolute;
  width: 50rem;
  height: 8rem;
  top: 35rem;
  left: 20rem;

  flex-shrink: 0; 
  border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1); 
  /* 正式版本 */
  background: rgb(220, 138, 241);
}

</style>