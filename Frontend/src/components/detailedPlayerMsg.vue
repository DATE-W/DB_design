<template>
  <my-nav></my-nav>
  <div class="basicinfoContainer">
    <img class="playerPic" :src="playerPhoto">
    <div class="basicInfo">
      <p class="header">
        {{ this.playerName }}
      </p>

      <div class="firstBar">
        <div class="firstBlock">
          俱乐部：{{ this.club }}
        </div>
        <div class="secondBlock">
          位置：{{ this.position }}
        </div>
        <div class="thirdBlock">
          号码：{{ this.no }}号
        </div>
      </div>

      <div class="secondBar">
        <div class="firstBlock">
          国籍：{{ this.nationality }}
        </div>
        <div class="secondBlock">
          年龄：{{ this.age }}岁
        </div>
        <div class="thirdBlock">
          生日：{{ this.birthday }}
        </div>
      </div>

      <div class="thirdBar">
        <div class="firstBlock">
          身高：{{ this.height }}CM
        </div>
        <div class="secondBlock">
          体重：{{ this.weight }}KG
        </div>
        <div class="thirdBlock">
          惯用脚：{{ this.dominantFoot }}
        </div>
      </div>

    </div>
  </div>

  <div class="eventInfo">
    <div style="font-size: 2vw;margin-left: 42%;">
      比赛数据
    </div>

    <div class="seasonInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        赛季
      </p>
    </div>

    <div class="clubInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        俱乐部
      </p>
    </div>

    <div class="playedTimeInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        上场
      </p>
    </div>

    <div class="startTimeInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        首发
      </p>
    </div>

    <div class="goalInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        进球
      </p>
    </div>

    <div class="assistsInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        助攻
      </p>
    </div>

    <div class="yellowCardInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        黄牌
      </p>
    </div>

    <div class="redCardInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        红牌
      </p>
    </div>

  </div>

  <div class="eventData" v-for="(event, index) in eventData" :key="index" :style="{
    top: `${index * 1.9 + 31}rem`,background: ((index % 2 === 0) ? 'wheat' : 'aqua')}">
    <div class="seasonInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.season }}
      </p>
    </div>

    <div class="clubInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.club }}
      </p>
    </div>

    <div class="playedTimeInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.playedTime }}
      </p>
    </div>

    <div class="startTimeInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.startTime }}
      </p>
    </div>

    <div class="goalInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.goal }}
      </p>
    </div>

    <div class="assistsInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.assists }}
      </p>
    </div>

    <div class="yellowCardInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.yellowCard }}
      </p>
    </div>

    <div class="redCardInfo">
      <p style="position: absolute;left: 50%;  transform: translate(-50%, -50%);">
        {{ event.redCard }}
      </p>
    </div>
  </div>

  <div class="honorHeader">
    <p class="header">荣誉记录</p>
  </div>

  <div class="honorData" v-for="(honor,index) in honors" :key="index" :style="{top:`${index*3+8}rem`}">
    <div class="honorName">
      <p style="position: absolute;left: 50%; transform: translate(-50%, -50%);">
        {{ honor.detailedHonor }}
      </p>
    </div>

    <div class="honorSeason">
      <p style="position: absolute;left: 50%; transform: translate(-50%, -50%);">
        {{ honor.season }}
      </p>
    </div>
  </div>

</template>

<script>
import MyNav from './nav.vue';

export default {
  components: {
    'my-nav': MyNav
  },

  created() {
    this.playerName = this.$route.query.playerName;
  },

  mounted() {

  },

  data() {
    return {
      playerName: 'mhy',
      playerPhoto: '/src/assets/img/wyh.png',
      club: '罗德岛',
      position: '后端',
      no: 1,
      nationality: '蒙德',
      age: 114,
      birthday: '1919-8-10',
      height: 150,
      weight: 150,
      dominantFoot: '轮椅',
      eventData: [
        { "season": '1-2', "club": '301', "playedTime": 3, "startTime": 4, "goal": 5, "assists": 6, "yellowCard": 7, "redCard": 8 },
        { "season": '1-2', "club": '301', "playedTime": 3, "startTime": 4, "goal": 5, "assists": 6, "yellowCard": 7, "redCard": 8 },

      ],
      honors: [
        { "detailedHonor": '上wyh的床', "season": '7-4' },
        { "detailedHonor": '上wrb的床', "season": '7-5' },
      ],
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
  flex-shrink: 0;  /* 

  background: rgb(221, 245, 251);*/

}

.playerPic {
  margin-top: 30px;
  margin-left: 40px;
  width: 160px;
  height: 240px;
}

.eventInfo {
  position: absolute;
  top: 55vh;
  left: 10vw;
  width: 60vw;
  height: 10vh;
  background: rgb(240, 240, 240);
}

.eventData {
  position: absolute;
  left: 10vw;
}

/* 信息第一行 */
.firstBar {
  position: absolute;
  left: 3vw;
  top: 9vh;
  height: 4vh;
  width: 36vw;
  /* 
  background: rgb(0, 240, 249);*/
}

/* 信息第二行 */
.secondBar {
  position: absolute;
  left: 3vw;
  top: 15vh;
  height: 4vh;
  width: 36vw;  /* 

  background: rgb(0, 240, 249);*/
}

/* 信息第三行 */
.thirdBar {
  position: absolute;
  left: 3vw;
  top: 22vh;
  height: 4vh;
  width: 36vw;  /*

  background: rgb(0, 240, 249); */
}

/* 横向第一条 */
.firstBlock {
  position: absolute;
  left: 1vw;  /* 

  background: rgb(255, 255, 255);*/
}

/* 横向第二条 */
.secondBlock {
  position: absolute;
  left: 13vw;  /* 

  background: rgb(255, 255, 255);*/
}

/* 横向第三条 */
.thirdBlock {
  position: absolute;
  left: 25vw;  /* 

  background: rgb(255, 255, 255);*/
}

.seasonInfo {
  position: absolute;
  width: 12vw;
  height: 4vh;
}

.clubInfo {
  position: absolute;
  left: 12vw;
  width: 12vw;
  height: 4vh;
}

.playedTimeInfo {
  position: absolute;
  left: 24vw;
  width: 6vw;
  height: 4vh;
}

.startTimeInfo {
  position: absolute;
  left: 30vw;
  width: 6vw;
  height: 4vh;
}

.goalInfo {
  position: absolute;
  left: 36vw;
  width: 6vw;
  height: 4vh;
}

.assistsInfo {
  position: absolute;
  left: 42vw;
  width: 6vw;
  height: 4vh;
}

.yellowCardInfo {
  position: absolute;
  left: 48vw;
  width: 6vw;
  height: 4vh;
}

.redCardInfo {
  position: absolute;
  left: 54vw;
  width: 6vw;
  height: 4vh;
}

.header {
  position: absolute;
  font-size: 2vw;
  left: 50%;
  transform: translate(-50%, -50%);
}

.honorHeader {
  position: absolute;
  left: 75%;
  width: 16vw;
  height: 8vh;
  background: rgb(240, 240, 240);
}

.honorData {
  position: absolute;
  left: 75%;
}

.honorName {
  position: absolute;
  width: 11vw;
  left: -2vw;
  height: 6vh;
}

.honorSeason {
  position: absolute;
  width: 9vw;
  left: 9vw;
  height: 6vh;
}
</style>