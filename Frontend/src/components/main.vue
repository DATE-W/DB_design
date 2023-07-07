<template>
<my-nav></my-nav>
    <el-header>
    
    </el-header>
    <el-main>
    <div class="main-container">
      <div class="top-section">
        <el-menu class="Games-menu"
            mode="horizontal" 
            active-text-color= "#409eff"
            @select="handleMenuSelect">
            <el-menu-item index="1" @click="GamesAll">全部</el-menu-item>
            <el-menu-item index="2" @click="GamesZC">中超</el-menu-item>
            <el-menu-item index="3" @click="GamesYC">英超</el-menu-item>
            <el-menu-item index="4" @click="GamesYJ">意甲</el-menu-item>
            <el-menu-item index="5" @click="GamesXJ">西甲</el-menu-item>
            <el-menu-item index="6" @click="GamesDJ"> 德甲</el-menu-item>
            <el-menu-item index="7" @click="GamesFJ">法甲</el-menu-item>
        </el-menu>
        <el-row class="Game-col-container">
          <el-card class="Game-card" v-for="item in getLimitedGames()" :key=item.index>
          <template #header>
            <div class="card-header">
            <span>Card name</span>
            <el-button class="button" text>更多</el-button>
            </div>
          </template>
          <div class="Game-content">
              <div class="column-status">{{item.status}}</div>
              <div class="column-team1">{{item.team1}} <br> {{ item.score1 }}</div>
              <div class="column-team1">{{item.team2}} <br> {{ item.score2 }}</div>
            </div>
          </el-card>
        </el-row>
      </div>
      <div class="bottom-section">
        <div class="left-side">左侧</div>
        <div class="right-side">右侧</div>
      </div>
    </div>
  </el-main>
</template>
  
<script>
  import MyNav from './nav.vue';
  
  export default {
    data(){
      return{
      GamesMsg: [
        {Game:"其他",time:"",status:"已结束",team1:"中国",score1:74,team2:"澳大利亚",score2:60},
        {Game:"其他",time:"",status:"已结束",team1:"中国",score1:74,team2:"澳大利亚",score2:60},
        {Game:"其他",time:"",status:"已结束",team1:"中国",score1:74,team2:"澳大利亚",score2:60},
        {Game:"其他",time:"",status:"已结束",team1:"中国",score1:74,team2:"澳大利亚",score2:60},
        {Game:"其他",time:"",status:"已结束",team1:"中国",score1:74,team2:"澳大利亚",score2:60},
        {Game:"其他",time:"",status:"已结束",team1:"中国",score1:74,team2:"澳大利亚",score2:60},
        {Game:"其他",time:"",status:"已结束",team1:"中国",score1:74,team2:"澳大利亚",score2:60},
        {Game:"其他",time:"",status:"已结束",team1:"中国",score1:74,team2:"澳大利亚",score2:60}
      ],
      maxItems:6
      }
    },
    components: {
      'my-nav': MyNav
    },
    methods: {
      getLimitedGames() {
        return this.GamesMsg.slice(0, this.maxItems);
      },
      GamesAll(){
        //选择全部赛事
      },
      GamesZC(){
        //选择中超赛事
      },
      GamesYC(){
        //选择英超赛事
      },
      GamesYJ(){
        //选择意甲赛事
      },
      GamesXJ(){
        //选择西甲赛事
      },
      GamesDJ(){
        //选择德甲赛事
      },
      GamesFJ(){
        //选择法甲赛事
      }   
    }
  };
</script>

<style>
.main-container {
  display: flex;
  flex-direction: column;
  height: 100%;
  position: relative;
}
.Games-menu{
  font-size: 16px;
}

.top-section {
  flex: 1;
  background-color: #f0f0f0;
}
.Game-col-container{
  display: flex;
  flex-wrap: nowrap; /* 防止列折行 */
}
.bottom-section {
  display: flex;
  flex: 2;
}
.Game-col{
  flex: 0 0 20%; /* 一行显示六个列，每个列占比16.66% */
  padding: 10px;
}
.left-side,
.right-side {
  flex: 1;
  background-color: #d0d0d0;
}
</style>
  
<!-- <button @click="this.$router.push('/signin')">登录</button>
<button @click="this.$router.push('/signup')">注册</button> -->
