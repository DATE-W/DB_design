<!-- 2154314_郑楷_个人主页 2023.07.07 16:00 v1.0.0
 v1.0.0 画出页面的基本模板，未填充具体内容，只做了组件定位-->

<template>
    <my-nav></my-nav>

    <!-- 左侧一列 -->
    <!-- 用户信息与邮箱 -->
    <border-box class="bBox" style="width: 200px;height: 60px; top: 11vh;left:3vw">

      <div class="userNameLayout" style="left:4vw;bottom:2vh">
        <p class="userNameTypo"> Karrigan</p>
      </div>

      <div class="userNameLayout" style="left:2.5vw;top:3.5vh">
        <p class="userMailTypo"> 996084341@qq.com</p>
      </div>

      <button class="goinBtn" style="right:1vw;top:2vh">
        <svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg" 
        style="position:absolute;right:0px;bottom: 0px;">
        <path d="M5 14L11 8L5 2" stroke="#161616" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
        </svg> 
      </button>

    </border-box>

    <border-box class="bBox" style="width: 200px;height:140px;top: 22vh;left:3vw">
    </border-box>

    <border-box class="bBox" style="width: 200px;height:200px;top: 47vh;left:3vw">
    </border-box>

    <border-box class="bBox" style="width: 200px;height: 60px; bottom: 11vh;left:3vw">
    </border-box>

    <!-- 中间一列 -->
    <!-- 主标题：个人主页 -->
    <div class="titleLayout" style="left:22vw;top:5vh">
        <p class="titleh1Typo">个人主页</p>
    </div>
    <!-- 中间列标题：动态 -->
    <div class="titleLayout" style="left:22vw;bottom:62vh">
        <p class="titleTypo">动态</p>
    </div>
    <!-- 中间列内容 -->
    <border-box class="bBox" style="width: 800px;height: 90px;  bottom: 50vh;left:21vw">
    </border-box>

    <border-box class="bBox" style="width: 800px;height: 90px;  bottom: 35vh;left:21vw">
    </border-box>

    <border-box class="bBox" style="width: 800px;height: 90px;  bottom: 20vh;left:21vw">
    </border-box>

    <border-box class="bBox" style="width: 800px;height: 90px;  bottom: 5vh;left:21vw">
    </border-box>

    <!-- 右侧一列 -->
    <div class="titleLayout" style="left:78vw;bottom:83vh">
        <p class="titleTypo">关注</p>
    </div>
    <border-box class="bBox" style="width: 300px;height: 120px; bottom: 7vh;right:3vw">
    </border-box>

    <border-box class="bBox" style="width: 300px;height: 120px; bottom: 27vh;right:3vw">
    </border-box>

    <border-box class="bBox" style="width: 300px;height: 120px; bottom: 47vh;right:3vw">
    </border-box>

    <border-box class="bBox" style="width: 300px;height: 120px; bottom: 67vh;right:3vw">
    </border-box>


</template>




<script>
import MyNav from './nav.vue';

export default{
    components: {
      'my-nav': MyNav
    }
}
</script>

<style scoped>
/* 装入按钮的容器框样式 */
.bBox{
  position: absolute;
  flex-shrink: 0;
  border-radius: 32px;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA); 
  /* 正式版本用此颜色*/
  /* background-color: #ffffff; */
  /* 方便检测位置*/
  background-color: #c3c3c3; 
}
/* 按钮样式 */
.btn1{
  position:absolute;
}
/* 文字风格 */
.userNameLayout{
  position:absolute;
  display: flex;
  width: 156px;
  flex-direction: column;
  flex-shrink: 0; 
}
.userNameTypo{
  color: var(--colors-text-dark-172239100, #172239);
  font-family: Verdana;
  font-size: 16px;
  font-style: normal;
  font-weight: 600;
  line-height: 24px;
}
.userMailTypo{
  color: var(--colors-text-dark-6-d-6-d-78100, #6D6D78);
  font-family: Verdana;
  font-size: 12px;
  font-style: normal;
  font-weight: 400;
  line-height: 16px;
}
.titleLayout
{
  position: absolute;
  display: flex;
  width: 940px;
  flex-direction: column;
  flex-shrink: 0; 
}
.titleTypo
{
  color: var(--colors-text-dark-172239100, #172239);
  font-family: Verdana;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: 32px; 
}
.titleh1Typo
{
  color: var(--colors-text-dark-172239100, #172239);
  font-family: Verdana;
  font-size: 40px;
  font-style: normal;
  font-weight: 600;
  line-height: 48px; 
}
/* 按钮风格 */
.goinBtn{
  position: absolute;
  width: 20px;
  height: 20px;
  flex-shrink: 0; 
}
</style>