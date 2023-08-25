<template>
  <my-nav></my-nav>
  <el-card class="basicinfoContainer">
    <img class="playerPic" :src="playerPhoto">
    <div class="basicInfo">
      <p class="header">
        {{ this.playerName }}
      </p>

      <div class="firstBar">
        <div class="firstBlock">
          年龄：{{ this.age }}
        </div>
        <div class="secondBlock">
          位置：{{ this.position }}
        </div>
        <div class="thirdBlock">
          号码：{{ this.number }}
        </div>
      </div>

      <div class="secondBar">
        <div class="firstBlock">
          国籍：{{ this.nationality }}
        </div>
        <div class="secondBlock">
          身高：{{ this.height }}
        </div>
        <div class="thirdBlock">
          惯用脚：{{ this.dominantFoot }}
        </div>
      </div>

      <div class="thirdBar">
        <div class="firstBlock">
          俱乐部：{{ this.club }}
        </div>
      </div>

    </div>
  </el-card>

  <div class="eventInfo">
    <div style="font-size: 2vw;margin-left: 42%;">
      比赛数据
    </div>

    <div class="seasonInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        赛季
      </p>
    </div>

    <div class="appearanceInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        上场
      </p>
    </div>

    <div class="passInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        过人
      </p>
    </div>

    <div class="shootInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        射门
      </p>
    </div>

    <div class="goalInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        进球
      </p>
    </div>

    <div class="assistInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        助攻
      </p>
    </div>

    <div class="yellowInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        黄牌
      </p>
    </div>

    <div class="redInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        红牌
      </p>
    </div>

  </div>

  <div class="detailedEventData" v-for="(event, index) in eventData" :key="index"
    :style="{ top: `${index * 2.5 + 31.5}rem` }">
    <div class="seasonInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.seasonName }}
      </p>
    </div>

    <div class="appearanceInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.appearance }}
      </p>
    </div>

    <div class="passInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.pass }}
      </p>
    </div>

    <div class="shootInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.shoot }}
      </p>
    </div>

    <div class="goalInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.goal }}
      </p>
    </div>

    <div class="assistInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.assist }}
      </p>
    </div>

    <div class="yellowInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.yellow }}
      </p>
    </div>

    <div class="redInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.red }}
      </p>
    </div>
  </div>

  <div class="relatedPlayerHeader">
    <p class="header">相关队员</p>
  </div>

  <div class="relatedPlayerData" v-for="(relatedPlayer, index) in relatedPlayers" :key="index" :style="{ top: `${index * 3 + 8.5}rem` }">
    <div class="relatedPlayerPhotoContainer" ><!--:class="{ oddIndex: index % 2 === 1, evenIndex: index % 2 === 0 }"-->
      <img class="relatedPlayerPhoto" :src="relatedPlayer.playerPhoto">
    </div>

    <div class="relatedPlayerNameContainer">
        {{ relatedPlayer.playerName }}
    </div>

    <div class="relatedPlayerPositionContainer" >
        {{ relatedPlayer.type }}
    </div>


  </div>
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

  },

  data() {
    return {
      playerName: 'mhy',
      enName: '114',
      playerPhoto: '/src/assets/img/wyh.png',
      club: '罗德岛',
      position: '后端',
      number: '1',
      nationality: '蒙德',
      age: '114',
      height: '150',
      dominantFoot: '轮椅',
      shoot: '1',
      pass:'2',

      eventData: ref([
        { "seasonName": '1-2',  "appearance": 3, "goal": 5, "assist": 6, "yellow": 7, "red": 8 },
        { "seasonName": '1-2',  "appearance": 3, "goal": 5, "assist": 6, "yellow": 7, "red": 8 },

      ]),

      relatedPlayers: ref([
        { "playerPhoto": '/src/assets/img/wyh.png', "playerName": 'wyh', "type": '前端' },
        { "playerPhoto": '/src/assets/img/wyh.png', "playerName": 'wyh', "type": '前端' },

      ]),

    }
  }


}
</script>

<style scoped>
/* 球员基本信息 */
.basicinfoContainer {
  position: absolute;
  left: 10%;
  width: 60vw;
  height: 20vw;
  flex-shrink: 0;
  background: rgb(240, 240, 240);

}

.basicInfo {
  position: absolute;
  top: 15%;
  left: 25%;
  width: 40vw;
  height: 15vw;
  flex-shrink: 0;
  /* 

  background: rgb(221, 245, 251);*/

}

.playerPic {
  margin-top: 30px;
  margin-left: 30px;
  width: 200px;
  height: 220px;
}

.eventInfo {
  position: absolute;
  top: 55vh;
  left: 10vw;
  width: 60vw;
  height: 10vh;
  background: rgb(240, 240, 240);
}

.detailedEventData {
  position: absolute;
  left: 10vw;
  background: wheat;
}

/* 信息第一行 */
.firstBar {
  position: absolute;
  left: 5vw;
  top: 9vh;
  height: 4vh;
  width: 36vw;
  /* 
  background: rgb(0, 240, 249);*/
}

/* 信息第二行 */
.secondBar {
  position: absolute;
  left: 5vw;
  top: 15vh;
  height: 4vh;
  width: 36vw;
  /* 

  background: rgb(0, 240, 249);*/
}

/* 信息第三行 */
.thirdBar {
  position: absolute;
  left: 5vw;
  top: 22vh;
  height: 4vh;
  width: 36vw;
  /*

  background: rgb(0, 240, 249); */
}

/* 横向第一条 */
.firstBlock {
  position: absolute;
  left: 1vw;
  /* 

  background: rgb(255, 255, 255);*/
}

/* 横向第二条 */
.secondBlock {
  position: absolute;
  left: 13vw;
  /* 

  background: rgb(255, 255, 255);*/
}

/* 横向第三条 */
.thirdBlock {
  position: absolute;
  left: 25vw;
  /* 

  background: rgb(255, 255, 255);*/
}

.seasonInfo {
  position: absolute;
  width: 11vw;
  height: 4vh;
}

.appearanceInfo {
  position: absolute;
  left: 11vw;
  width: 7vw;
  height: 4vh;
}

.passInfo {
  position: absolute;
  left: 18vw;
  width: 7vw;
  height: 4vh;
}

.shootInfo {
  position: absolute;
  left: 25vw;
  width: 7vw;
  height: 4vh;
}

.goalInfo {
  position: absolute;
  left: 32vw;
  width: 7vw;
  height: 4vh;
}

.assistInfo {
  position: absolute;
  left: 39vw;
  width: 7vw;
  height: 4vh;
}

.yellowInfo {
  position: absolute;
  left: 46vw;
  width: 7vw;
  height: 4vh;
}

.redInfo {
  position: absolute;
  left: 53vw;
  width: 7vw;
  height: 4vh;
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