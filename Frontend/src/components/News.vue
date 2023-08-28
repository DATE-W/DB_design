<template>
  <div class="total">
    <!-- 顶部导航栏 -->
    <my-nav></my-nav>
    <div class="line" style="left:2%; width: 45%;height: 0.05px;"></div>
    <div class="common-layout">
      <el-container>
        <!-- 新闻页面头栏 -->
        <el-header>
          <div class="common-layout">
            <el-container>
              <el-header>
                <!-- 导航栏logo -->
                <div>
                  <a class="logo">FootBall</a>
                  <b class="title">足坛新闻</b>
                  <!-- 搜索栏 -->
                  <el-input class="search" v-model="searchInput" placeholder=" 请 输 入 搜 索 关 键 字">
                    <template v-slot:prepend>
                      <el-button icon="Search" @click="search"></el-button>
                    </template>
                  </el-input>
                  <img src="../assets/img/recover_logo.png" class="img" style="left: 48%;">
                  <img src="../assets/img/football_logo.png" class="img" style="left: 49%;">
                </div>
              </el-header>
              <div class="line" style="left:1%; width: 102%;height: 3px;"></div>
              <!-- 新闻导航栏 -->
              <el-main>
                <!-- <li @click="activeTab = 'home'" :class="{ 'active': activeTab === 'home' }">Home</li>  @select="handleMenuSelect"-->
                <el-menu mode="horizontal" active-text-color="#409eff">
                  <el-menu-item @click="activeTab = 'home'" :class="{ 'active': activeTab === 'home' }"
                    class="menu">首页</el-menu-item>
                  <el-menu-item @click="() => { activeTab = 'China'; switchLeague('中超') }"
                    :class="{ 'active': activeTab === 'China' }" class="menu">中超</el-menu-item>
                  <el-menu-item @click="() => { activeTab = 'England'; switchLeague('英超') }"
                    :class="{ 'active': activeTab === 'England' }" class="menu">英超</el-menu-item>
                  <el-menu-item @click="() => { activeTab = 'Spain'; switchLeague('西甲') }"
                    :class="{ 'active': activeTab === 'Spain' }" class="menu">西甲</el-menu-item>
                  <el-menu-item @click="() => { activeTab = 'Germany'; switchLeague('德甲') }"
                    :class="{ 'active': activeTab === 'Germany' }" class="menu">德甲</el-menu-item>
                  <el-menu-item @click="() => { activeTab = 'Italy'; switchLeague('意甲') }"
                    :class="{ 'active': activeTab === 'Italy' }" class="menu">意甲</el-menu-item>
                  <el-menu-item @click="() => { activeTab = 'France'; switchLeague('法甲') }"
                    :class="{ 'active': activeTab === 'France' }" class="menu">法甲</el-menu-item>
                </el-menu>
              </el-main>
            </el-container>
          </div>
        </el-header>

        <!-- 下面将根据导航栏选择的界面进行展示 (搜索结果也囊括在内)-->

        <!-- 首页 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'home'">
          <!-- 左侧部分 -->
          <el-aside width="30vw">
            <!-- 走马灯 -->
            <div>
              <el-carousel :interval="3000" class="Carousel">
                <el-carousel-item v-if="carouselRecommendItems.length != 0"
                  v-for="(item, index) in carouselRecommendItems" :key="index">
                  <img v-if="matchMP4(item.pictureRoutes[0]) == false" referrerPolicy='no-referrer'
                    :src="item.pictureRoutes" alt="carousel image" class="imgANO" @click="openNewsDetails(item)">
                  <video v-if="matchMP4(item.pictureRoutes[0]) == true" referrerPolicy='no-referrer' ref="videoPlayer"
                    :src="item.pictureRoutes[0]" class="imgANO" @click="openNewsDetails(item)" controls />
                  <div class="description" @click="openNewsDetails(item)">{{
                    truncateText(truncateText(item.newsBody.title, 16), 16) }}</div>
                </el-carousel-item>
              </el-carousel>
            </div>
            <!-- 推荐模块展示内容（配图） -->
            <div class="imgList1">
              <el-row>
                <el-col :span="12" v-if="recommendItems1.length != 0" v-for="(item, index) in recommendItems1"
                  :key="index">
                  <div class="imgItem1">
                    <img v-if="matchMP4(item.pictureRoutes[0]) == false" referrerPolicy='no-referrer'
                      :src="item.pictureRoutes" alt="Image" class="imgRecommend">
                    <video v-if="matchMP4(item.pictureRoutes[0]) == true" referrerPolicy='no-referrer' ref="videoPlayer"
                      :src="item.pictureRoutes[0]" class="imgRecommend" controls />
                    <div class="descriptionRecommend">{{ truncateText(item.newsBody.title, 16) }}</div>
                  </div>
                </el-col>
              </el-row>
            </div>
            <!-- 热门视频部分 -->
            <div class="video">
              <p class="titleLeft">热门视频</p>
              <a class="moreLeft" @click="openVideoMore()">更多 ></a>
              <div class="line" style="width: 24.5vw;height: 0.2px;top:-4vh;left:7.5%;"></div>
              <div class="LeftCarousel">
                <!-- 走马灯 -->
                <el-carousel :interval="3000" class="CarouselRight">
                  <el-carousel-item v-if="recommendItems1.length != 0" v-for="(item, index) in carouselVideoItems"
                    :key="index">
                    <img referrerPolicy='no-referrer' :src="item.cover" alt="carousel image" class="imgANO"
                      @click="openLink(item.urlink)">
                    <div class="description" @click="openLink(item.urllink)">{{ truncateText(item.title, 16) }}
                    </div>
                  </el-carousel-item>
                </el-carousel>
              </div>
              <!-- 热门视频模块展示内容（配图） -->
              <div class="imgList1" style="top:-2vh">
                <el-row>
                  <el-col :span="12" v-if="recommendItems1.length != 0" v-for="(item, index) in videoItems1" :key="index">
                    <div class="imgItem1">
                      <img referrerPolicy='no-referrer' :src="item.cover" alt="Image" class="imgRecommend"
                        @click="openLink(item.urlink)">
                      <div class="descriptionRecommend" @click="openLink(item.urllink)">{{ truncateText(item.title,
                        16) }}</div>
                    </div>
                  </el-col>
                </el-row>
              </div>
              <!-- 热点视频模块展示内容（文字标题） -->
              <div class="videoText">
                <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in videoItems2" :key="index">
                  <div class="textItem">
                    <el-icon style="font-size: 23px;" @click="openLink(item.urlink)">
                      <VideoPlay />
                    </el-icon>
                    <a class="Text" @click="openLink(item.link)">{{ truncateText(item.title, 16) }}</a>
                  </div>
                </el-row>
              </div>
            </div>

          </el-aside>
          <!-- 中间和右侧部分 -->
          <el-main width="70vw">
            <div class="common-layout">
              <el-container>
                <!-- 中间部分 -->
                <el-aside width="30vw">
                  <div class="center">
                    <!-- 热门推荐部分 -->
                    <div class="recommendText">
                      <p class="CenterMainTitle">热门推荐</p>
                      <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in recommendItems2" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 中超新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">中超</p>
                      <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in ChinaItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 英超新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">英超</p>
                      <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in ENGLANDItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 西甲新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">西甲</p>
                      <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in SpainItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 德甲新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">德甲</p>
                      <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in GermanyItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 意甲新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">意甲</p>
                      <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in ItalyItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}</a>
                        </div>
                      </el-row>
                    </div>

                  </div>

                </el-aside>

                <!-- 右侧部分 -->
                <el-main class="Right">
                  <p class="titleRight">球坛八卦</p>
                  <a class="moreRight" @click="openGossipMore()">更多 ></a>
                  <div class="line" style="width: 22vw;height: 0.2px;top:-9vh;left:-3%;"></div>
                  <div class="RightCarousel">
                    <!-- 走马灯 -->
                    <el-carousel :interval="3000" class="CarouselRight">
                      <el-carousel-item v-if="recommendItems1.length != 0" v-for="(item, index) in carouselGossipItems"
                        :key="index">
                        <img v-if="matchMP4(item.pictureRoutes[0]) == false" referrerPolicy='no-referrer'
                          :src="item.pictureRoutes[0]" alt="carousel image" class="imgANO" @click="openLink(item.link)">
                        <video v-if="matchMP4(item.pictureRoutes[0]) == true" referrerPolicy='no-referrer'
                          ref="videoPlayer" :src="item.pictureRoutes[0]" class="imgANO" controls />
                        <div class="description" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}
                        </div>
                      </el-carousel-item>
                    </el-carousel>
                  </div>
                  <!-- 图文八卦新闻 -->
                  <div class="imgList2">
                    <div v-if="recommendItems1.length != 0" v-for="(item, index) in gossipItems1" :key="index"
                      class="imgItem2">
                      <el-row :gutter="20">
                        <el-col :span="11">
                          <img v-if="matchMP4(item.pictureRoutes[0]) == false" referrerPolicy='no-referrer'
                            :src="item.pictureRoutes[0]" alt="Image" class="imgGossip">
                          <video v-if="matchMP4(item.pictureRoutes[0]) == true" referrerPolicy='no-referrer'
                            ref="videoPlayer" :src="item.pictureRoutes[0]" class="imgGossip" controls />
                        </el-col>
                        <el-col :span="8">
                          <div class="descriptionGossip">{{ truncateText(item.newsBody.title, 16) }}</div>
                        </el-col>
                      </el-row>
                    </div>
                  </div>
                  <!-- 八卦推荐部分 -->
                  <div class="RightNews">
                    <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in gossipItems2" :key="index">
                      <div class="textItem" style="top:-10vh">
                        <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                          <ToiletPaper />
                        </el-icon>
                        <a class="Text" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}</a>
                      </div>
                    </el-row>
                  </div>
                  <div class="line" style="width: 24.5vw;height: 0.2px;top:-4vh;left:-2.5%;"></div>
                  <el-icon class="logoex">
                    <Cherry />
                  </el-icon>
                  <!-- 法甲新闻 -->
                  <div class="RightNews">
                    <p class="CenterTitle">法甲</p>
                    <el-row v-if="recommendItems1.length != 0" v-for="(item, index) in FranceItems" :key="index">
                      <div class="textItem" style="top:-10vh">
                        <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                          <Football />
                        </el-icon>
                        <a class="Text" @click="openLink(item.link)">{{ truncateText(item.newsBody.title, 16) }}</a>
                      </div>
                    </el-row>
                  </div>
                </el-main>

              </el-container>
            </div>
          </el-main>

        </el-container>

        <!-- 联赛选择 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab != 'home' && activeTab != 'Search'">
          <div class="containerLeague">
            <el-row>
              <!-- 左侧动态 -->
              <el-col :span="18" class="leftLeague">
                <p class="titleLeagueLeft">{{ LeagueTitle1 }}</p>
                <div class="line" style="width: 40vw;height: 0.2px;top:-12vh;left:9%;"></div>
                <div v-if="recommendItems1.length != 0" v-for="item in LeagueNews" :key="item.id" class="itemLeague">
                  <div class="imgWrapper">
                    <img v-if="matchMP4(item.pictureRoutes[0]) == false" referrerPolicy='no-referrer'
                      :src="item.pictureRoutes[0]" alt="Image" class="imgSearch">
                    <video v-if="matchMP4(item.pictureRoutes[0]) == true" referrerPolicy='no-referrer' ref="videoPlayer"
                      :src="item.pictureRoutes[0]" class="imgSearch" controls />
                  </div>
                  <div class="TextWrapper">
                    <div class="titleSearch">{{ truncateText(item.newsBody.title, 20) }}</div>
                    <div class="descriptionSearch">{{ item.newsBody.summary }}</div>
                  </div>
                </div>
                <div class="NoMore">No More ......</div>
              </el-col>
              <!-- 右侧球队一览+排行榜 -->
              <el-col :span="6" class="rightLeague" :class="{ 'fixedLeague': isFixed }">
                <div class="floating-block">
                  <p class="titleLeagueLeft" style="font-size: 25px;left:3%;">{{ LeagueTitle2 }}</p>
                  <div class="line" style="width: 22vw;height: 0.2px;top:-12.5vh;left:3%;"></div>
                  <div class="Teamcontainer">
                    <el-row :gutter="10" v-if="recommendItems1.length != 0" v-for="(row, index) in LeagueTeam.length"
                      :key="index">
                      <el-col :span="6" v-if="recommendItems1.length != 0"
                        v-for="(item, index) in LeagueTeam.slice((row - 1) * 4, row * 4)" :key="index">
                        <img referrerPolicy='no-referrer' :src="item.teamLogo" class="imgTeam" alt="image" />
                        <div class="textTeam">{{ truncateText(item.teamName, 6) }}</div>
                      </el-col>
                    </el-row>
                  </div>
                </div>
                <div class="remaining-content">
                  <p class="titleLeagueLeft" style="font-size: 25px;left:-15%;">射手榜</p>
                  <div class="line" style="width: 22vw;height: 0.2px;top:-13vh;left:-15%;"></div>
                  <el-table :data="LeagueShooter" stripe style="width: 150%" class="shooter">
                    <el-table-column label="排名" type="index" width="55" />
                    <el-table-column prop="playerName" label="球员姓名" width="120" />
                    <el-table-column prop="teamName" label="所属球队" width="120" />
                    <el-table-column prop="goals" label="进球数" />
                  </el-table>
                </div>
              </el-col>
            </el-row>
          </div>
        </el-container>

        <!-- 搜索结果 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'Search'">
          <el-row>
            <!-- 搜索新闻结果（位于左侧） -->
            <p class="titleLeagueLeft" style="left:3vw;">搜索结果</p>
            <div class="line" style="width: 40vw;height: 0.2px;top:4vh;left:-19vw;"></div>
            <el-col :span="18">
              <div v-if="searchNewsResults.length != 0" v-for="item in searchNewsResults" :key="item.id"
                class="itemSearch">
                <div class="imgWrapper">
                  <img v-if="item.pictureRoutes != null && matchMP4(item.pictureRoutes[0]) == false"
                    referrerPolicy='no-referrer' :src="item.pictureRoutes[0]" alt="Image" class="imgSearch">
                  <video v-if="item.pictureRoutes != null && matchMP4(item.pictureRoutes[0]) == true"
                    referrerPolicy='no-referrer' ref="videoPlayer" :src="item.pictureRoutes[0]" class="imgSearch"
                    controls />
                </div>
                <div class="TextWrapper">
                  <div v-if="item.newsBody != null" class="titleSearch">{{ truncateText(item.newsBody.title, 20) }}
                  </div>
                  <div v-if="item.newsBody != null" class="descriptionSearch">{{ item.newsBody.summary }}</div>
                </div>
              </div>
              <div class="NoMore">No More ......</div>
            </el-col>
            <!-- 搜索视频结果（位于右侧） -->
            <el-col :span="6">
              <p class="titleRight" style="font-size: 25px; top:-18vh">搜索视频</p>
              <el-icon class="iconSearch">
                <VideoPlay />
              </el-icon>
              <div class="line" style="width: 22vw;height: 0.2px;top:-23vh;left:-3%;"></div>
              <div class="imgListSearch">
                <div v-if="searchVideoResults.length != 0" v-for="(item, index) in searchVideoResults" :key="index"
                  class="imgItemSearch">
                  <el-row :gutter="20">
                    <el-col :span="11">
                      <img v-if="item.cover != null" referrerPolicy='no-referrer' :src="item.cover" alt="Image"
                        class="imgVideoSearch">
                    </el-col>
                    <el-col :span="8">
                      <div v-if="item.title != null" class="descriptionVideoSearch">{{ truncateText(item.title, 20) }}
                      </div>
                    </el-col>
                  </el-row>
                </div>
              </div>
              <div class="NoMore" style="top:-6vh">No More ......</div>
            </el-col>
          </el-row>
        </el-container>

      </el-container>
    </div>
  </div>
  <div class="line" style="left:5%; width: 80%;height: 0.05px;"></div>

  <newsdetails v-if="Detail == null" :items="Detail"></newsdetails>
</template>

<script>
import MyNav from './nav.vue';
import { ElInput, useTransitionFallthroughEmits } from 'element-plus';
import { ElCarousel, ElCarouselItem } from 'element-plus';
import { ElMessage } from 'element-plus';
import axios from 'axios';
import NewsDetails from './NewsDetails.vue';

export default {
  components: {
    'my-nav': MyNav,
    'newsdetails': NewsDetails,
    ElCarousel,
    ElCarouselItem
  },
  data() {
    return {
      ok: '',

      Detail: [], //新闻详情内容，传递给NewsDetails.vue进行渲染

      //当前联赛
      LeagueTitle1: '',  //**动态
      LeagueTitle2: '',  //**球队一览

      activeTab: 'home', // 新闻导航栏默认选中的选项
      isFixed: false,    //用于判断联赛界面的右侧悬浮

      //以下为搜索数据
      searchInput: '',    //搜索框的内容
      searchNewsResults: [],    //搜索结果
      searchVideoResults: [],   //搜索结果

      //以下为主页面所需数据
      carouselRecommendItems: [],  //推荐模块走马灯显示图片及内容
      carouselVideoItems: [],   //热门视频走马灯显示图片及内容
      carouselGossipItems: [],  //八卦走马灯显示图片及内容
      recommendItems1: [],  //推荐模块展示内容（配图）
      recommendItems2: [], //推荐模块展示内容（不配图）
      videoItems1: [], //视频模块展示内容（配图）
      videoItems2: [],  //视频模块展示内容（不配图）
      gossipItems1: [], //八卦模块展示内容（配图）
      gossipItems2: [], //八卦模块展示内容（不配图）
      ChinaItems: [],//中超模块展示内容
      ENGLANDItems: [],  //英超模块展示内容
      FranceItems: [],//法甲模块展示内容
      ItalyItems: [], //意甲模块展示内容
      GermanyItems: [], //德甲模块展示内容
      SpainItems: [],  //西甲模块展示内容

      //以下为选择联赛后所需数据
      LeagueNews: [],    //联赛模块展示新闻
      LeagueTeam: [],//联赛队伍队徽
      LeagueShooter: [],   //联赛队伍射手榜

      tmp: [],
    }
  },

  //进入页面后调取接口，从而获得一组数据
  created() {
    this.getData(4, '', '普通', this.carouselRecommendItems);    //主页面左上角走马灯
    this.getData(2, '', '普通', this.recommendItems1);          //主页面左上角走马灯下推荐内容
    this.getData(2, '', '普通', this.recommendItems2);          //主页面中部推荐内容
    // this.getData(8, '', '普通', this.tmp);
    // this.carouselRecommendItems = this.tmp.slice(0, 4);
    // this.recommendItems1 = this.tmp.slice(4, 6);
    // this.recommendItems2 = this.tmp.slice(6, 8);

    this.getVideoData(4, '', '', this.carouselVideoItems);      //主页面视频走马灯
    this.getVideoData(2, '', '', this.videoItems1);            //主页面视频图文
    this.getVideoData(5, '', '', this.videoItems2);            //主页面视频纯文字
    // this.getVideoData(11, '', '', this.tmp);
    // this.carouselVideoItems = this.tmp.slice(0, 4);
    // this.videoItems1 = this.tmp.slice(4, 6);
    // this.videoItems2 = this.tmp.slice(6, 11);

    this.getData(4, '中超', '普通', this.ChinaItems);            //主页面中超新闻
    this.getData(4, '英超', '普通', this.ENGLANDItems);          //主页面英超新闻
    // this.getData(4, '西甲', '普通', this.SpainItems);          //主页面西甲新闻
    // this.getData(4, '德甲', '普通', this.GermanyItems);          //主页面德甲新闻
    // this.getData(4, '意甲', '普通', this.ItalyItems);          //主页面意甲新闻
    // this.getData(4, '法甲', '普通', this.FranceItems);          //主页面法甲新闻

    this.getData(4, '', '八卦', this.carouselGossipItems);      //主页面右侧八卦走马灯
    this.getData(3, '', '八卦', this.gossipItems1);      //主页面右侧八卦图文
    this.getData(5, '', '八卦', this.gossipItems2);      //主页面右侧八卦纯文字
    //   this.getData(12, '', '八卦', this.tmp);
    //   this.carouselGossipItems = this.tmp.slice(0, 4);
    //   this.gossipItems1 = this.tmp.slice(4, 7);
    //   this.gossipItems2 = this.tmp.slice(7, 12);
  },

  mounted() {
    window.addEventListener('scroll', this.handleScroll);
  },
  beforeDestroy() {
    window.removeEventListener('scroll', this.handleScroll);
  },

  methods: {
    search() {
      this.activeTab = 'Search';
      // 执行搜索逻辑
      this.performSearch(this.searchInput);
    },
    async performSearch(keyword) {
      try {
        const requestData = {
          keyword: String(keyword),
        };

        const response1 = await axios.post('/api/News/SearchNews', requestData, {
          headers: {
            'Content-Type': 'application/json',
          },
        }); // 发送POST请求，并将请求数据作为 JSON 对象发送
        console.log(response1.data.value);

        const response2 = await axios.post('/api/Video/SearchVideo', requestData, {
          headers: {
            'Content-Type': 'application/json',
          },
        }); // 发送POST请求，并将请求数据作为 JSON 对象发送

        // console.log(response2.data.value);

        // 将数组存贮于传入的数组名中
        this.searchNewsResults = response1.data.value;
        this.searchVideoResults = response2.data.value;
        console.log('123');
        console.log(this.searchNewsResults);
        // console.log(this.searchVideoResults);
      } catch (error) {
        console.error(error);
      }
    },

    handleScroll() {
      const scrollTop = window.scrollY;
      const threshold = 2000; // 右侧部分滑动到多少位置时触发悬浮效果

      this.isFixed = scrollTop >= threshold;
    },

    //再图片路径前面加服务器地址获取完成url
    getimgurl(urlSrc) {
      return 'http://110.40.206.206' + urlSrc;
    },
    //打开链接的页面
    openLink(url) {
      window.open(url, '_blank');
    },
    //跳转到八卦页面
    openGossipMore() {
      this.openLink('/NewsGossip');
      // this.$router.push('/NewsGossip');
    },
    //跳转到视频页面
    openVideoMore() {
      this.openLink('./NewsVideo');
      // this.$router.push('/NewsVideo');
    },
    //根据联赛名称返回其int值
    returnLeagueInt(LeagueName) {
      if (LeagueName == '中超')
        return 6;
      else if (LeagueName == '英超')
        return 1;
      else if (LeagueName == '西甲')
        return 2;
      else if (LeagueName == '意甲')
        return 3;
      else if (LeagueName == '德甲')
        return 4;
      else if (LeagueName == '法甲')
        return 5;
    },

    //从后端获取新闻数据
    async getData(newsQuantity, tag1, tag2, dataItems) {
      try {
        const requestData = {
          num: newsQuantity,
          matchTag: String(tag1),
          propertyTag: String(tag2),
        };

        const response = await axios.post('/api/News/GetNews', requestData, {
          headers: {
            'Content-Type': 'application/json',
          },
        }); // 发送POST请求，并将请求数据作为 JSON 对象发送

        console.log(response.data.value);
        console.log(tag1);

        this.ok = response.data.ok;
        // 将数组存贮于传入的数组名中
        dataItems.splice(0, dataItems.length, ...response.data.value);
        console.log(dataItems);
        // console.log(this.items);
      } catch (error) {
        console.error(error);
      }
    },
    //从后端获取视频数据
    async getVideoData(newsQuantity, tag1, tag2, dataItems) {
      try {
        const requestData = {
          num: newsQuantity,
          matchTag: String(tag1),
          propertyTag: String(tag2),
        };

        const response = await axios.post('/api/Video/GetVideoRandomly', requestData, {
          headers: {
            'Content-Type': 'application/json',
          },
        }); // 发送POST请求，并将请求数据作为 JSON 对象发送

        // console.log(response.data.value);

        // 将数组存贮于传入的数组名中
        dataItems.splice(0, dataItems.length, ...response.data.value);
        // console.log(dataItems);
        // console.log(this.items);
      } catch (error) {
        console.error(error);

        ElMessage({
          message: err.response.data.msg,
          grouping: false,
          type: 'error',
        })

        return
      }
    },
    //从后端读取联赛队伍名称+队徽(1-6——英超西甲意甲德甲法甲中超)
    async getTeamName(gameTypeInt) {
      try {
        const requestData = {
          gameType: gameTypeInt,
        };

        const response = await axios.post('/api/updateTeam/searchTeamInGameType', requestData, {
          headers: {
            'Content-Type': 'application/json',
          },
        }); // 发送POST请求，并将请求数据作为 JSON 对象发送

        // console.log(response.data);

        // 将数组存贮于传入的数组名中
        this.LeagueTeam.splice(0, this.LeagueTeam.length, ...response.data);
        // console.log(dataItems);
        // console.log(this.LeagueTeam);
      } catch (error) {
        console.error(error);
        ElMessage({
          message: err.response.data.msg,
          grouping: false,
          type: 'error',
        })
        return
      }
    },
    //从后端读取联赛射手榜
    async getShooter(gameType) {
      try {
        const requestData = {
          gameName: String(gameType),
        };

        const response = await axios.post('/api/updateTeam/topScorersInGameType', requestData, {
          headers: {
            'Content-Type': 'application/json',
          },
        }); // 发送POST请求，并将请求数据作为 JSON 对象发送

        // console.log(response.data);

        // 将数组存贮于传入的数组名中
        this.LeagueShooter.splice(0, this.LeagueShooter.length, ...response.data);
        // console.log(dataItems);
        // console.log(this.LeagueShooter);
      } catch (error) {
        console.error(error);
        ElMessage({
          message: err.response.data.msg,
          grouping: false,
          type: 'error',
        })
        return
      }
    },

    //截断过长的文本
    truncateText(text, maxLength) {
      if (text.length > maxLength) {
        return text.substring(0, maxLength) + '...';
      }
      return text;
    },

    //匹配mp4字符，用于判断视频还是图片的渲染
    matchMP4(str) {
      return str.includes('mp4');
    },

    //切换联赛展示
    switchLeague(gameType) {
      this.LeagueTitle1 = gameType + '动态';
      this.LeagueTitle2 = gameType + '球队一览';
      //更改联赛数组
      this.getData(-1, gameType, '', this.LeagueNews);
      this.getTeamName(this.returnLeagueInt(gameType));
      this.getShooter(gameType);
    },

    //打开新闻详情页
    openNewsDetails(item) {
      // console.log('123');
      // console.log(item);
      this.Detail = item;
      // console.log(this.Detail);
      this.openLink('/NewsDetails');
    }
  }
}
</script>

<style>
.total {
  position: relative;
  width: 85%;
  height: auto;
  left: 5%;
  /* background-color: rgb(239, 239, 239); */
}

.line {
  background: #000000;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25) inset;
  position: relative;
}

.logo {
  position: relative;
  top: 1vh;
  left: 2%;
  flex-shrink: 0;
  color: #000;
  text-align: center;
  font-family: 'Trebuchet MS', 'Lucida Sans Unicode', 'Lucida Grande', 'Lucida Sans', Arial, sans-serif;
  font-size: 3.5vh;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.title {
  position: relative;
  top: 1vh;
  left: 7%;
  flex-shrink: 0;
  color: #000;
  text-align: center;
  font-family: FZYaoTi;
  font-size: 30px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.search {
  position: relative;
  top: 0.5vh;
  left: 45%;
  width: 420px;
  height: 37px;
  flex-shrink: 0;
  border-radius: 5px;
  background: #f8f5f5;
  border: 0;
  font-size: 15px;
}

.menu {
  position: relative;
  top: -1vh;
  left: 1%;
  color: #000;
  text-align: center;
  font-family: Microsoft YaHei;
  font-size: 20px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
}

.img {
  top: 2vh;
  width: 35px;
  height: 35px;
  position: relative;
}

.imgANO {
  left: 0%;
  width: 25vw;
  height: 35vh;
  position: relative;
}

/* <--以下是首页样式--> */
/* 左上走马灯 */
.Carousel {
  left: 8%;
  width: 80%;
  height: 80%;
  position: relative;
}

.CarouselRight {
  left: 0%;
  width: 80%;
  height: 80%;
  position: relative;
}

/* 左上走马灯描述 */
.description {
  position: absolute;
  bottom: 0;
  left: 0%;
  width: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  color: white;
  padding: 8px;
}

/* 左侧图文样式板块 */
.imgList1 {
  position: relative;
  width: 80%;
  left: 8%;
}

.imgItem1 {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin: 10px;
}

/* 右侧图文样式板块 */
.imgList2 {
  display: flex;
  flex-direction: column;
  position: relative;
  top: -6vh;
  left: -0.6vh;
}

.imgItem2 {
  display: flex;
  align-items: flex-start;
  margin-bottom: 10px;
}

/* 左上推荐部分图片样式 */
.imgRecommend {
  top: 0vh;
  width: 12vw;
  height: 15vh;
  position: relative;
}

/* 左上推荐部分图片描述 */
.descriptionRecommend {
  width: 105%;
  height: 105%;
  flex-shrink: 0;
  color: #000;
  font-family: Mogra;
  font-size: 14px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  position: relative;
  top: 1vh;
  left: 1%
}

/* 左侧热门视频标题 */
.titleLeft {
  width: 90px;
  height: 23px;
  flex-shrink: 0;
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  position: relative;
  top: -11%;
  left: 8.2%;
}

/* 左侧更多>跳转样式 */
.moreLeft {
  width: 15px;
  height: 15px;
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 10px;
  font-style: normal;
  font-weight: 290;
  line-height: normal;
  position: relative;
  top: -4.1vh;
  left: 80%;
}

/* 右侧更多>跳转样式 */
.moreRight {
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 12px;
  font-style: normal;
  font-weight: 290;
  line-height: normal;
  position: relative;
  top: -9vh;
  left: 67%;
}

/* 右侧大布局 */
.Right {
  width: 30vw;
  position: relative;
  top: -2.1vh;
}

/* 右侧八卦标题 */
.titleRight {
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  position: relative;
  top: -4.6vh;
  left: -2.5%;
}

/* 右侧走马灯 */
.RightCarousel {
  position: relative;
  top: -7.5vh;
  left: -2%;
}

/* 右侧图文样式 */
.RightImage {
  position: relative;
  top: -6vh;
}

/* 八卦推荐部分图片描述 */
.descriptionGossip {
  width: 100%;
  height: 40%;
  flex-shrink: 0;
  color: #000;
  font-family: Mogra;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  position: relative;
  left: -5%;
  top: 0vh;
}

/* 八卦推荐部分图片样式 */
.imgGossip {
  top: 0vh;
  left: -2%;
  width: 12vw;
  height: 15vh;
  position: relative;
}

/* 左侧走马灯 */
.LeftCarousel {
  position: relative;
  top: -2vh;
  left: 8%;
}

.center {
  position: relative;
  left: 0%;
  top: -2.5vh;
}

/* 热点视频文字样式 */
.videoText {
  position: relative;
  left: 7%;
  top: -0.8vh;
}

/* 推荐文字样式 */
.recommendText {
  position: relative;
  left: -2%;
  top: -5%;
}

.Text {
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 17px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  flex-shrink: 0;
  position: relative;
  left: 4%;
  top: -5%;
}

.textItem {
  display: flex;
  margin: 8px;
}

.CenterNews {
  position: relative;
  left: -2%;
  top: 1vh;
}

.RightNews {
  position: relative;
  left: -4%;
  top: -5vh;
}

.CenterMainTitle {
  position: relative;
  left: 2%;
  top: 0vh;
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}

.CenterTitle {
  position: relative;
  left: 2%;
  top: 1vh;
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 20px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
}

.video {
  position: relative;
  top: 3vh;
}

.logoex {
  position: relative;
  top: -2.5vh;
  left: 40%;
  font-size: 33px;
}

/* <--首页样式结束--> */


/* <--<--以下是搜索结果样式-->--> */
/* <--新闻搜索结果--> */
.itemSearch {
  display: flex;
  /* align-items: center; */
  margin-bottom: 10px;
  position: relative;
  left: 10%;
  top: -5vh;
  margin-top: 5%;
}

.imgWrapper {
  margin-right: 10px;
  width: 60%;
}

.TextWrapper {
  display: flex;
  flex-direction: column;
}

/* 图片样式 */
.imgSearch {
  top: 0vh;
  left: 0%;
  width: 30vw;
  height: 30vh;
  position: relative;
}

/* 标题样式 */
.titleSearch {
  font-weight: bold;
  width: 22vw;
  /* 设置容器的宽度 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
  /* white-space: nowrap; */
  /* white-space: nowrap;  不换行 */
  margin-top: 5px;
  font-size: 30px;
  position: relative;
  top: 2vh;
  left: -1vw;
}

/* 描述样式 */
.descriptionSearch {
  width: 20vw;
  /* 设置容器的宽度 */
  max-height: 120px;
  /* 设置最大高度 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
  margin-top: 5px;
  font-size: 17px;
  position: relative;
  left: -1vw;
  top: 4vh;
}

/* <--新闻搜索结果结束--> */

/* <--视频搜索结果--> */
.iconSearch {
  font-size: 30px;
  position: relative;
  top: -23.5vh;
  left: 90%;
}

.imgVideoSearch {
  top: 0vh;
  left: -2%;
  width: 10vw;
  height: 12vh;
  position: relative;
}

.descriptionVideoSearch {
  flex-shrink: 0;
  color: #000;
  font-family: Mogra;
  font-size: 18px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  position: relative;
  left: 2vw;
  top: 0vh;
  width: 8vw;
  /* 设置容器的宽度 */
  max-height: 80%;
  /* 设置最大高度 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
}

/* <--视频搜索结果结束--> */
.NoMore {
  color: #000;
  text-align: center;
  font-family: STSong;
  font-size: 16px;
  font-style: normal;
  font-weight: 400;
  line-height: normal;
  position: relative;
  top: 5%
}

.imgListSearch {
  display: flex;
  flex-direction: column;
  position: relative;
  top: -24vh;
  left: -0.7vh;
}

.imgItemSearch {
  display: flex;
  align-items: flex-start;
  margin-bottom: 10px;
  position: relative;
  top: 3vh;
}

/* <--<--搜索结果样式结束-->--> */
/* <--<--以下是联赛样式-->--> */
.containerLeague {
  display: flex;
}

.leftLeague {
  flex: 1;
  height: 100vh;
}

.rightLeague {
  flex: 1;

  position: relative;
  /* 设置为相对定位，以便内部元素使用绝对定位 */
  height: 30vh;
  left: 18vw;
}

.fixedLeague {
  position: fixed;
  /* 设置为固定定位 */
  top: 0;
  /* 固定在页面顶部 */
  right: 0;
  /* 固定在页面右侧 */
}

.floating-block {
  position: absolute;
  /* 设置为绝对定位 */
  top: 25px;
  /* 设置悬浮块的初始位置 */
  right: 50px;
  /* 设置悬浮块的初始位置 */

}

.remaining-content {
  margin-top: 120vh;
  /* 为了避免悬浮块覆盖内容，设置内容的上边距 */
  height: 20vh;
}

.titleLeagueLeft {
  width: 364px;
  height: 76px;
  flex-shrink: 0;
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 36px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  position: relative;
  left: 6vw;
  top: -6vh;
}

.itemLeague {
  display: flex;
  /* align-items: center; */
  margin-bottom: 10px;
  position: relative;
  left: 9%;
  top: -15vh;
  margin-top: 5vh;
}

.Teamcontainer {
  position: relative;
  top: -2vh;
}

.imgTeam {
  position: relative;
  top: -11vh;
  left: 0vh;
  width: 5vw;
  height: 5vw;
  margin-left: 15px;
  margin-top: 15px;
}

.textTeam {
  flex-shrink: 0;
  color: #000;
  font-family: Microsoft YaHei;
  font-size: 15px;
  font-style: normal;
  font-weight: 700;
  line-height: normal;
  position: relative;
  top: -11vh;
  left: 2vh;
  position: center;
  white-space: nowrap;
  /* 不换行 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
}

.shooter {
  position: relative;
  top: -12vh;
  left: -3.5vw;
  width: 10vw;
}

/* <--<--联赛样式结束-->--> */
</style>