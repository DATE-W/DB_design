<template>
  <my-nav></my-nav>
  <el-card class="playerNameAndIcon">
    <img class="playerIconContainer" alt="This is player icon" :src="this.playerPhoto">
    <div class="playerNameContainer">
      {{ this.playerName }}
    </div>
    <div class="playerEnnameContainer">
      {{ this.enName }}
    </div>
  </el-card>

  <el-card class="playerInfo">
    <div class="firstBar">
      <div class="firstBlock">
        国籍：{{ this.nationality }}
      </div>
      <div class="secondBlock">
        年龄：{{ this.age }}
      </div>
      <div class="thirdBlock">
        身高：{{ this.height }}
      </div>
    </div>

    <div class="secondBar">
      <div class="firstBlock">
        号码：{{ this.number }}
      </div>
      <div class="secondBlock">
        位置：{{ this.position }}
      </div>

      <div class="thirdBlock">
        俱乐部：{{ this.club }}
      </div>
    </div>

    <div class="thirdBar">
      <div class="firstBlock">
        惯用脚：{{ this.dominantFoot }}
      </div>
    </div>

    <div class="briefIntroductionContainer">
      球员简介：{{ this.playerName }}，（英文名：{{ this.enName }}），出生于{{ 2023 - this.age }}年，是一名{{ this.nationality }}
      的足球运动员。他身高{{ this.height }}，现效力于{{ this.club }}足球俱乐部。他的惯用脚是{{ this.dominantFoot }}，
      在球队中担任{{ this.position }}的位置。他的球衣号码为{{ this.number }}。
    </div>
  </el-card>

  <div class="titleContainer">
    <div class="titleBar"></div>
    <div class="title">
      <p style="transform: translateY(-30%); ">赛季数据</p>
    </div>
  </div>

  <el-table :data="eventData" style="width: 60vw; left: 10vw; top: 70vh;">
    <el-table-column align="center" prop="seasonName" label="赛季" width="150" />
    <el-table-column align="center" prop="appearance" label="上场" width="105" />
    <el-table-column align="center" prop="pass" label="过人" width="105" />
    <el-table-column align="center" prop="shoot" label="射门" width="105" />
    <el-table-column align="center" prop="goal" label="进球" width="105" />
    <el-table-column align="center" prop="assist" label="助攻" width="105" />
    <el-table-column align="center" prop="yellow" label="黄牌" width="105" />
    <el-table-column align="center" prop="red" label="红牌" width="105" />
  </el-table>

  <div class="titleContainer2">
    <div class="titleBar"></div>
    <div class="title">
      <p style="transform: translateY(-30%); ">相关球员</p>
    </div>
  </div>

  <!---->
  <table class="relatedTable">
    <tbody>
      <tr style v-for="(relatedPlayer, index) in relatedPlayers" :key="index" @click="direct2detailedPlayerMsg(relatedPlayer.playerName)">
        <td class="tdTable1" :style="{ top: `${index * 3 + 0.6}rem`}">
          <div>
            <img :src="relatedPlayer.playerPhoto" style="width: 2vw; height: 4vh;">
          </div>
        </td>

        <td class="tdTable2" :style="{ top: `${index * 3 + 0.6}rem`}">
          <div style="text-align: left;">
            {{ relatedPlayer.playerName }}
          </div>

          
        </td>
        <td class="tdTable3" :style="{ top: `${index * 3 + 0.6}rem`}">
          <div>
            {{ relatedPlayer.type }}
          </div>

        </td>
      </tr>
    </tbody>
  </table>

  <!--
  <el-table :data="relatedPlayers" style="width: 10vw; left: 75vw; top: -18vh;">
    <el-table-column label="头像" width="150">    
      <template slot-scope="scope">
        <el-image style="width: 50px; height: 50px;" :src="scope.row.playerPhoto">

        </el-image>
      </template>
    </el-table-column>
    <el-table-column prop="type"  width="150" />
  </el-table>

-->

</template>

<script>
import { ref } from 'vue';
import MyNav from './nav.vue';
import { ElMessage } from 'element-plus';
import axios from 'axios';

export default {
  components: {
    'my-nav': MyNav
  },

  created() {
    this.playerName = this.$route.query.playerName;
  },

  mounted() {
    this.getPlayerMsg(this.playerName);
  },

  methods: {
    async getPlayerMsg(playerName) {
      this.playerName = playerName;
      let response;
      try {
        response = await axios.post('/api/updateTeam/getPlayerDetail', {
          playerName: playerName,
        });

        console.log(response);

        this.relatedPlayers = [];
        this.eventData = [];

        this.enName = response.data.enName;
        this.playerPhoto = response.data.photo;
        this.club = response.data.club;
        this.position = response.data.position;
        this.number = response.data.number;
        this.nationality = response.data.nationality;
        this.age = response.data.age;
        this.height = response.data.height;
        this.dominantFoot = response.data.dominantFoot;
        this.shoot = response.data.shoot;
        this.pass = response.data.pass;

        this.relatedPlayers = response.data.relatedPlayer;
        this.eventData = response.data.eventData;

      } catch (err) {
        ElMessage({
          message: '获取球员信息失败',
          type: 'error',
        });
      }

    },

    direct2detailedPlayerMsg(topScorerName) {
      this.$router.push({
        path: '/detailedPlayerMsg',
        query: {
          playerName: topScorerName
        }
      });
      this.getPlayerMsg(topScorerName);
    },


  },

  data() {
    return {
      playerName: 'mhy',
      enName: 'wangyuehui',
      playerPhoto: '/src/assets/img/wyh.png',
      club: '罗德岛',
      position: '后端',
      number: '1',
      nationality: '蒙德',
      age: '114',
      height: '150',
      dominantFoot: '轮椅',
      shoot: '1',
      pass: '2',

      eventData: ref([
        { "seasonName": '1-2', "appearance": 3, "pass": 1, "shoot": 3, "goal": 5, "assist": 6, "yellow": 7, "red": 8 },
        { "seasonName": '1-2', "appearance": 3, "pass": 1, "shoot": 3, "goal": 5, "assist": 6, "yellow": 7, "red": 8 },
        { "seasonName": '1-2', "appearance": 3, "pass": 1, "shoot": 3, "goal": 5, "assist": 6, "yellow": 7, "red": 8 },
        { "seasonName": '1-2', "appearance": 3, "pass": 1, "shoot": 3, "goal": 5, "assist": 6, "yellow": 7, "red": 8 },

      ]),

      relatedPlayers: ref([
        { "playerPhoto": '/src/assets/img/wyh.png', "playerName": 'wh', "type": '1端' },
        { "playerPhoto": '/src/assets/img/wyh.png', "playerName": 'wyh', "type": '前端' },

      ]),

    }
  }


}
</script>

<style scoped>
.playerNameAndIcon {
  position: absolute;
  left: 10vw;
  width: 60vw;
  height: 20vh;
  flex-shrink: 0;
}

.playerIconContainer {
  position: absolute;
  left: 4vw;
  top: 4vh;
  width: 8vw;
  height: 12vh;
  flex-shrink: 0;
}

.playerNameContainer {
  position: absolute;
  left: 15vw;
  top: 4vh;
  font-size: 2.5vw;
}

.playerEnnameContainer {
  position: absolute;
  left: 15vw;
  top: 11vh;
  font-size: 1.5vw;
}

.playerInfo {
  position: absolute;
  left: 10vw;
  top: 32vh;
  width: 60vw;
  height: 35vh;
  flex-shrink: 0;
}

/* 信息第一行 */
.firstBar {
  position: absolute;
  left: 4vw;
  top: 5vh;
  height: 4vh;
  width: 60vw;
}

/* 信息第二行 */
.secondBar {
  position: absolute;
  left: 4vw;
  top: 11vh;
  height: 4vh;
  width: 60vw;
}

/* 信息第三行 */
.thirdBar {
  position: absolute;
  left: 4vw;
  top: 17vh;
  height: 4vh;
  width: 60vw;
}

/* 横向第一条 */
.firstBlock {
  position: absolute;
  left: 1vw;
}

/* 横向第二条 */
.secondBlock {
  position: absolute;
  left: 16vw;
}

/* 横向第三条 */
.thirdBlock {
  position: absolute;
  left: 30vw;
}

.briefIntroductionContainer {
  position: absolute;
  left: 3vw;
  top: 23vh;
  height: 13vh;
  width: 55vw;
}

.titleContainer {
  position: absolute;
  border: none;
  top: 70vh;
  left: 10vw;
  width: 7vw;
  height: 6vh;
  background: rgb(240, 240, 240);
}

.titleBar {
  position: absolute;
  top: 1.5vh;
  width: 0.5vw;
  height: 3vh;
  background: aqua;
}

.title {
  position: absolute;
  left: 1.5vw;
  width: 10vw;
  height: 6vh;
}

.titleContainer2 {
  position: absolute;
  border: none;
  top: 8vh;
  left: 75vw;
  width: 7vw;
  height: 6vh;
  background: rgb(240, 240, 240);
}

.relatedTable{
  position: absolute;
  top: 15vh;
  left: 75vw;
  width: 18vw;
}

.tdTable1{
  position: absolute;
  left: 1vw;
}

.tdTable2{
  position: absolute;
  left: 4vw;
}

.tdTable3{
  position: absolute;
  left: 12vw;
}










.header {
  position: absolute;
  font-size: 2vw;
  left: 50%;
  transform: translate(-50%, -50%);
}

.relatedPlayerHeader {
  position: absolute;
  left: 75%;
  width: 18vw;
  height: 8vh;
  background: rgb(240, 240, 240);
}

.relatedPlayerData {
  position: absolute;
  left: 75%;
}

.relatedPlayerPhotoContainer {
  position: absolute;
  width: 3vw;
  height: 4vh;
}

.relatedPlayerPhoto {
  position: absolute;
  width: 1.5vw;
  height: 3vh;
  top: 40%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.relatedPlayerNameContainer {
  position: absolute;
  left: 3vw;
  width: 10vw;
  height: 4vh;
}

.relatedPlayerName {
  position: absolute;
  color: var(--colors-text-dark-172239100, #172239);
  left: 20%;
  top: -5%;
  transform: translate(-50%, -50%);
}

.relatedPlayerPositionContainer {
  position: absolute;
  left: 15vw;
  width: 5vw;
  height: 4vh;
}

.relatedPlayerName {
  position: absolute;
  color: var(--colors-text-dark-172239100, #172239);
  left: 20%;
  top: -5%;
  transform: translate(-50%, -50%);
}

.oddIndex {
  background-color: lightblue;
  /* 奇数行的背景色 */
}

.evenIndex {
  background-color: lightpink;
  /* 偶数行的背景色 */
}
</style>