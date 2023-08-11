<!-- 2154314_郑楷_赛事详情页 2023.08.11 14:00 v1.1.0 
v1.0.0 添加页面，改路由，完成赛事列表向该页的UID传值 
v1.1.0 向赛事列表主页面传值，使得之前的选择结果得到保存 -->

<template>

<my-nav></my-nav>

<el-button size="large" type="info" icon="back" @click="toGames(this.dateChoice,this.lgeChoice)">Back</el-button>

<!-- <p>{{ gameUid }}</p> -->


<div class="topTextBox">
  <p>{{ this.dateChoice }}&nbsp&nbsp&nbsp&nbsp{{ this.dateTime }}&nbsp&nbsp&nbsp&nbsp{{ this.lgeChoice }}</p>

</div>

</template>

<script>
import MyNav from './nav.vue';
import axios from 'axios';
import { ref } from 'vue';

export default {
  components: {
    'my-nav': MyNav
  },

  created() {
    this.gameUid = this.$route.query.gameUid;
    this.dateChoice = this.$route.query.dateChoice;
    this.lgeChoice = this.$route.query.lgeChoice;
  },

  methods: {
    toGames(dateChoice,lgeChoice)
    {
        this.$router.push(
        {
          path:`/Games`,
          query:{      
            date11: dateChoice,
            league11 :lgeChoice,   
          }
        }
      );
    },
  },

  data() {
    return {
      lgeChoice:0,
      dateChoice:'',

      /* gameUid: ref(''),
      dateTime: ref(''),
      homeTeamName: ref(''),
      guestTeamName: ref(''),
      status: ref(0),
      homeScore: ref(0),
      guestScore: ref(0),
      homeRecentGames: ref([]),
      guestRecentGames: ref([]), */

      gameUid: 'PRD13403419',
      dateTime: '20:00',
      homeTeamName: '利物浦',
      guestTeamName: '曼联',
      status: '2',
      homeScore: '7',
      guestScore: '0',
      homeRecentGames: [],
      guestRecentGames: [],

    }
    
  }
}
</script>

<style scoped>
.topTextBox
{
display: grid;
position: absolute;
width: 17rem;
height: 10rem;
flex-shrink: 0; 
justify-content: center;
left: 40rem;
top:5rem;
/* left: 50vw; */
/* 测试版本 */
background: rgb(241, 69, 69);
}



</style>