<template>
  <my-nav></my-nav>
  <div class="basicinfoContainer">
    <img src="../assets/img/otto.png" class="playerPic">
    <div class="basicInfo">
      <p>个人基本信息</p>
    </div>
  </div>

  <div class="competitionInfo">
    <p>比赛数据</p>
  </div>
</template>

<script>
import MyNav from './nav.vue';

export default {
  components: {
    'my-nav': MyNav
  },


  data() {
    return {
      club: '',
      position: '',
      no: 0,
      nationality: '',
      age: 0,
      birthday: '',
      height: 0,
      weight: 0,
      dominantFoot: '',
      eventData: [
        { "season": '', "club": '', "playedTime": 0, "startTime": 0, "goal": 0, "assists": 0, "yellowCard": 0, "redCard": 0 },
      ],
      honors: [
        { "detailedHonor": '', "season": '' },
      ],
    }
  }


}
</script>

<style scoped>
/* 球员基本信息 */
.basicinfoContainer {
  position: absolute;
  left: 20%;
  width: 60vw;
  height: 20vw;
  flex-shrink: 0;
  background: rgb(230, 230, 230);

}

.basicInfo {
  position: absolute;
  top: 15%;
  left: 25%;
  width: 40vw;
  height: 15vw;
  flex-shrink: 0;
  background: rgb(230, 230, 230);

}


.playerPic {
  margin-top: 30px;
  margin-left: 40px;
  width: 160px;
  height: 240px;
}

.competitionInfo {
  margin-top: 350px;
  margin-left: 290px;
  width: 60vw;
  height: 240px;
  background: rgb(230, 230, 230);

}
</style>