<!-- 2154314_郑楷_个人主页 2023.07.23 21:00 v1.0.0
 v1.0.0 页面画了一半 -->

<template>
    <my-nav></my-nav>

    <!-- 左侧联赛选择器 -->
    <border-box class="borderBoxLeft" style="left:5rem" >

      <border-box class="borderBoxLeague" style="top:1rem" >
        <p class="textTypoLeague" style="left:2.5rem">全部赛事</p>
      </border-box>

      <border-box class="borderBoxLeague" style="top:5.8rem" >
        <p class="textTypoLeague">英超</p>
      </border-box>

      <border-box class="borderBoxLeague" style="top:10.6rem" >
        <p class="textTypoLeague">西甲</p>
      </border-box>

      <border-box class="borderBoxLeague" style="top:15.4rem" >
        <p class="textTypoLeague">意甲</p>
      </border-box>

      <border-box class="borderBoxLeague" style="top:20.2rem" >
        <p class="textTypoLeague">德甲</p>
      </border-box>

      <border-box class="borderBoxLeague" style="top:25rem" >
        <p class="textTypoLeague">法甲</p>
      </border-box>

      <border-box class="borderBoxLeague" style="top:30rem" >
        <p class="textTypoLeague">中超</p>
      </border-box>

      <border-box class="borderBoxLeague" style="top:35rem" >
        <p class="textTypoLeague" style="left:2.5rem">其他赛事</p>
      </border-box>
    </border-box>



    <border-box class="borderBoxMid" style="left:27rem">
    </border-box>

    <border-box class="borderBoxRightTop" style="right:5rem">
    </border-box>

    <border-box class="borderBoxRightAD" style="right:5rem;">
    </border-box>






</template>

<script>
import MyNav from './nav.vue';
export default{
    components: {
      'my-nav': MyNav
    },
    methods:{
      redirectToRegister() {
      }
    }
}
  

</script>


<style scoped>

/* 框体样式 */

/* 左侧容器框 */
.borderBoxLeft
{
  position:absolute;
  width: 15rem;
  height: 40rem;
  flex-shrink: 0; 
  /* 正式版本 */

  /* 测试版本 */
  background: #4BDFBC; 
}
/* 中部容器框 */
.borderBoxMid
{
  position:absolute;
  width: 40rem;
  height: 40rem;
  flex-shrink: 0; 
  /* 正式版本 */
  
  /* 测试版本 */
  background: #E174C3; 
}
/* 右侧上方容器框 */
.borderBoxRightTop
{
  position:absolute;
  width: 17rem;
  height: 15rem;
  flex-shrink: 0; 
  /* 正式版本 */
  
  /* 测试版本 */
  background: #4BDFBC; 
}
/* 右侧下方容器框 */
.borderBoxRightAD
{
  position:absolute;
  width: 17rem;
  height: 20rem;
  flex-shrink: 0;
  bottom: 0.2rem;
  /* 正式版本 */
  
  /* 测试版本 */
  background: #4B8FDF; 
}
/* 联赛选择按钮 */
.borderBoxLeague
{
  position:absolute;
  width: 13rem;
  height: 4rem;
  flex-shrink: 0; 
  border-radius: 2rem;
  border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA); 
  left:1rem;
  /* 正式版本 */
  /* background-color: #ffffff; */
  /* 测试版本 */
  background: var(--colors-primary-ffdfd-6100, #FFDFD6);
}
/* 联赛名称框 */
.borderBoxText1
{
  position:absolute;
  width: 8rem;
  height: 2rem;
  top: 1rem;
  left: 4rem;

  /* 测试版本，正式版本删去 */
  background: white;
}

/* 字体样式 */

/* 左侧联赛名称字体样式 */
.textTypoLeague
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




</style>