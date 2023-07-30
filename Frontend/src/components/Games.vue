<!-- 2154314_郑楷_赛事界面 2023.07.23 21:00 v1.0.0
 v1.0.0 页面画了一半 
 v1.1.0 画出了左侧的联赛选择器（未添加逻辑），布局了中部的比赛列表（已添加跳转逻辑），增加了各大联赛LOGO素材图，日期选择器和广告区待实现-->

 <template>
  <my-nav></my-nav>

  <!-- 左侧联赛选择器 -->
  <border-box class="borderBoxLeft" style="left:5rem" >

    <border-box class="borderBoxLeague" style="top:1rem" @click="leagueChoice(0)" @dblclick="leagueChoice(10)">
      <p class="textTypoLeague" style="left:2.5rem">全部赛事</p>
    </border-box>

    <border-box class="borderBoxLeague" style="top:5.8rem" @click="leagueChoice(1)">
      <img src="../assets/img/pmlogo.png" class="imgLogo" >
      <p class="textTypoLeague">英超</p>
    </border-box>

    <border-box class="borderBoxLeague" style="top:10.6rem" @click="leagueChoice(2)">
      <img src="../assets/img/lllogo.png" class="imgLogo" >
      <p class="textTypoLeague">西甲</p>
    </border-box>

    <border-box class="borderBoxLeague" style="top:15.4rem" @click="leagueChoice(3)">
      <img src="../assets/img/salogo.png" class="imgLogo" >
      <p class="textTypoLeague">意甲</p>
    </border-box>

    <border-box class="borderBoxLeague" style="top:20.2rem;" @click="leagueChoice(4)">
      <img src="../assets/img/bllogo.png" class="imgLogo" style="border-radius: 0rem; " >
      <p class="textTypoLeague">德甲</p>
    </border-box>

    <border-box class="borderBoxLeague" style="top:25rem" @click="leagueChoice(5)">
      <img src="../assets/img/le1logo.png" class="imgLogo" >
      <p class="textTypoLeague">法甲</p>
    </border-box>

    <border-box class="borderBoxLeague" style="top:30rem" @click="leagueChoice(6)">
      <img src="../assets/img/cslogo.png" class="imgLogo" style="left:1.5rem;top:0.2rem">
      <p class="textTypoLeague">中超</p>
    </border-box>

    <border-box class="borderBoxLeague" style="top:35rem" @click="leagueChoice(7)">
      <p class="textTypoLeague" style="left:2.5rem">其他赛事</p>
    </border-box>
  </border-box>


  <!-- 中间列时间与赛事列表 -->
  <border-box class="borderBoxMid" style="left:27rem">
    <p class="textDate">2023.08.12</p>

    <!-- test -->
    <p class="textTypoLeague" style="left:10rem">test:leaguechoice:{{league}}</p>

    <border-box class="borderBoxMatch" style="top:5rem" @click="toMatchDetail">
    </border-box>

    <border-box class="borderBoxMatch" style="top:11rem" @click="toMatchDetail">
    </border-box>

    <border-box class="borderBoxMatch" style="top:17rem" @click="toMatchDetail">
    </border-box>

    <border-box class="borderBoxMatch" style="top:23rem" @click="toMatchDetail">
    </border-box>

    <border-box class="borderBoxMatch" style="top:29rem" @click="toMatchDetail">
    </border-box>

    <border-box class="borderBoxMatch" style="top:35rem" @click="toMatchDetail">
    </border-box>

  </border-box>

  <border-box class="borderBoxRightTop" style="right:5rem">
    <p>日期选择</p>
  </border-box>

  <border-box class="borderBoxRightAD" style="right:5rem;">
    <p>广告</p>
  </border-box>






</template>

<script>
import { nextTick } from 'vue';
import MyNav from './nav.vue';
export default{
  components: {
    'my-nav': MyNav
  },
  methods:{
    toMatchDetail(){
      this.$router.push('/GamesDetail');
    },
    leagueChoice(choice){
      if(this.league!=choice){
        this.league=choice;
      }
      else{
        this.league=0;
      }
    },
  },
  data()
  {
    return{
      league:0,
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
width: 16vw;
height: 90vh;
flex-shrink: 0; 
/* 正式版本 */
background: rgb(240, 240, 240);
/* 测试版本 */
/* background: #4BDFBC;  */
}
/* 中部容器框 */
.borderBoxMid
{
position:absolute;
width: 40rem;
height: 40rem;
flex-shrink: 0; 
/* 正式版本 */
background:white;
/* 测试版本 */
/* background: #E174C3;  */
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
background-color: #ffffff;
transition:background-color 0.8s ease;
}
.borderBoxLeague:hover
{
background-color: rgb(246, 77, 77);
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
/* 各场赛事框 */
.borderBoxMatch
{
position:absolute;
width: 39.8rem;
height: 4rem;
flex-shrink: 0; 
border-radius: 1.4rem;
border: 0.05rem solid var(--colors-light-eaeaea-100, #d1d1d1); 
transition:background-color 0.8s ease;
/* 正式版本 */
background: white;
}
.borderBoxMatch:hover
{
background-color: rgb(240, 240, 240);
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
/* 中部日期字体样式 */
.textDate
{
position:absolute;
color: var(--colors-text-dark-172239100, #172239);
font-family: Verdana;
font-size: 1rem;
font-style: normal;
font-weight: 100;
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



</style>