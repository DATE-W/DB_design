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
                <!-- <li @click="activeTab = 'home'" :class="{ 'active': activeTab === 'home' }">Home</li> -->
                <el-menu mode="horizontal" active-text-color="#409eff" @select="handleMenuSelect">
                  <el-menu-item @click="activeTab = 'home'" :class="{ 'active': activeTab === 'home' }"
                    class="menu">首页</el-menu-item>
                  <el-menu-item @click="activeTab = 'China'" :class="{ 'active': activeTab === 'China' }"
                    class="menu">中超</el-menu-item>
                  <el-menu-item @click="activeTab = 'England'" :class="{ 'active': activeTab === 'England' }"
                    class="menu">英超</el-menu-item>
                  <el-menu-item @click="activeTab = 'Spain'" :class="{ 'active': activeTab === 'Spain' }"
                    class="menu">西甲</el-menu-item>
                  <el-menu-item @click="activeTab = 'Germany'" :class="{ 'active': activeTab === 'Germany' }"
                    class="menu">德甲</el-menu-item>
                  <el-menu-item @click="activeTab = 'Italy'" :class="{ 'active': activeTab === 'Italy' }"
                    class="menu">意甲</el-menu-item>
                  <el-menu-item @click="activeTab = 'France'" :class="{ 'active': activeTab === 'France' }"
                    class="menu">法甲</el-menu-item>
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
                <el-carousel-item v-for="(item, index) in carouselRecommendItems" :key="index">
                  <img :src="item.image" alt="carousel image" class="imgANO" @click="openLink(item.link)">
                  <div class="description" @click="openLink(item.link)">{{ item.description }}</div>
                </el-carousel-item>
              </el-carousel>
            </div>
            <!-- 推荐模块展示内容（配图） -->
            <div class="imgList1">
              <el-row>
                <el-col :span="12" v-for="(item, index) in recommendItems1" :key="index">
                  <div class="imgItem1">
                    <img :src="item.image" alt="Image" class="imgRecommend" @click="openLink(item.link)">
                    <div class="descriptionRecommend" @click="openLink(item.link)">{{ item.description }}</div>
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
                  <el-carousel-item v-for="(item, index) in carouselVideoItems" :key="index">
                    <img :src="item.image" alt="carousel image" class="imgANO" @click="openLink(item.link)">
                    <div class="description" @click="openLink(item.link)">{{ item.description }}</div>
                  </el-carousel-item>
                </el-carousel>
              </div>
              <!-- 热门视频模块展示内容（配图） -->
              <div class="imgList1" style="top:-2vh">
                <el-row>
                  <el-col :span="12" v-for="(item, index) in videoItems1" :key="index">
                    <div class="imgItem1">
                      <img :src="item.image" alt="Image" class="imgRecommend" @click="openLink(item.link)">
                      <div class="descriptionRecommend" @click="openLink(item.link)">{{ item.description }}</div>
                    </div>
                  </el-col>
                </el-row>
              </div>
              <!-- 热点视频模块展示内容（文字标题） -->
              <div class="videoText">
                <el-row v-for="(item, index) in videoItems2" :key="index">
                  <div class="textItem">
                    <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                      <VideoPlay />
                    </el-icon>
                    <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
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
                      <el-row v-for="(item, index) in recommendItems2" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 中超新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">中超</p>
                      <el-row v-for="(item, index) in ChinaItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 英超新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">英超</p>
                      <el-row v-for="(item, index) in ENGLANDItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 西甲新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">西甲</p>
                      <el-row v-for="(item, index) in SpainItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 德甲新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">德甲</p>
                      <el-row v-for="(item, index) in GermanyItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
                        </div>
                      </el-row>
                    </div>
                    <!-- 意甲新闻 -->
                    <div class="CenterNews">
                      <p class="CenterTitle">意甲</p>
                      <el-row v-for="(item, index) in ItalyItems" :key="index">
                        <div class="textItem">
                          <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                            <Football />
                          </el-icon>
                          <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
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
                      <el-carousel-item v-for="(item, index) in carouselGossipItems" :key="index">
                        <img :src="item.image" alt="carousel image" class="imgANO" @click="openLink(item.link)">
                        <div class="description" @click="openLink(item.link)">{{ item.description }}</div>
                      </el-carousel-item>
                    </el-carousel>
                  </div>
                  <!-- 图文八卦新闻 -->
                  <div class="imgList2">
                    <div v-for="(item, index) in gossipItems1" :key="index" class="imgItem2">
                      <el-row :gutter="20">
                        <el-col :span="11">
                          <img :src="item.image" alt="Image" class="imgGossip">
                        </el-col>
                        <el-col :span="8">
                          <div class="descriptionGossip">{{ item.description }}</div>
                        </el-col>
                      </el-row>
                    </div>
                  </div>
                  <!-- 八卦推荐部分 -->
                  <div class="RightNews">
                    <el-row v-for="(item, index) in gossipItems2" :key="index">
                      <div class="textItem" style="top:-10vh">
                        <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                          <ToiletPaper />
                        </el-icon>

                        <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
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
                    <el-row v-for="(item, index) in FranceItems" :key="index">
                      <div class="textItem" style="top:-10vh">
                        <el-icon style="font-size: 23px;" @click="openLink(item.link)">
                          <Football />
                        </el-icon>
                        <a class="Text" @click="openLink(item.link)">{{ item.description }}</a>
                      </div>
                    </el-row>
                  </div>
                </el-main>

              </el-container>
            </div>
          </el-main>

        </el-container>
        <!-- 中超 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'China'">
          <div class="containerLeague">
            <!-- 左侧动态 -->
            <div class="leftLeague">左侧内容</div>
            <!-- 右侧球队一览+排行榜+最新新闻 -->
            <div class="rightLeague" :class="{ 'fixedLeague': isFixed }">
              <div class="floating-block">悬挂内容</div>
              <div class="remaining-content">剩余内容</div>
            </div>
          </div>
          

          

        </el-container>
        <!-- 英超 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'England'">英超</el-container>
        <!-- 西甲 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'Spain'">西甲</el-container>
        <!-- 德甲 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'Germany'">德甲</el-container>
        <!-- 意甲 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'Italy'">意甲</el-container>
        <!-- 法甲 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'France'">法甲</el-container>
        <!-- 搜索结果 -->
        <el-container style="top:22vh;position: absolute;" v-if="activeTab === 'Search'">
          <el-row>
            <!-- 搜索新闻结果（位于左侧） -->
            <el-col :span="18">
              <div v-for="item in searchNewsResults" :key="item.id" class="itemSearch">
                <div class="imgWrapper">
                  <img :src="item.image" alt="Image" class="imgSearch">
                </div>
                <div class="TextWrapper">
                  <div class="titleSearch">{{ item.title }}</div>
                  <div class="descriptionSearch">{{ item.description }}</div>
                </div>
              </div>
              <div class="NoMore">No More ......</div>
            </el-col>
            <!-- 搜索视频结果（位于右侧） -->
            <el-col :span="6">
              <p class="titleRight" style="font-size: 25px;">相关视频</p>
              <el-icon class="iconSearch">
                <VideoPlay />
              </el-icon>
              <div class="line" style="width: 22vw;height: 0.2px;top:-10vh;left:-3%;"></div>
              <div class="imgListSearch">
                <div v-for="(item, index) in searchVideoResults" :key="index" class="imgItemSearch">
                  <el-row :gutter="20">
                    <el-col :span="11">
                      <img :src="item.image" alt="Image" class="imgVideoSearch">
                    </el-col>
                    <el-col :span="8">
                      <div class="descriptionVideoSearch">{{ item.title }}</div>
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
</template>

<script>
import MyNav from './nav.vue';
import { ElInput } from 'element-plus';
import { ElCarousel, ElCarouselItem } from 'element-plus';

import carousel1 from '../assets/img/carousel1.png';
import carousel2 from '../assets/img/carousel2.png';
import carousel3 from '../assets/img/carousel3.png';
import carousel4 from '../assets/img/carousel4.png';

export default {
  components: {
    'my-nav': MyNav,
    ElCarousel,
    ElCarouselItem
  },
  data() {
    return {
      activeTab: 'home', // 新闻导航栏默认选中的选项

      isFixed: false,    //用于判断联赛界面的右侧悬浮

      searchInput: '',    //搜索框的内容
      searchNewsResults: [    //搜索结果
        {
          image: carousel1,
          title: '新闻1的标题 1111',
          description: '搜索结果1正文可使用按钮触发下拉菜单。\
                        \设置 split-button 属性来让触发下拉元素呈现为按钮组，左边是功能按钮，右边是触发下拉菜单的按钮，设置为 true 即可。 如果你想要在第三和第四个选项之间添加一个分隔符，你只需要为第四个选项添加一个 divider 的 CSS class。',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel2,
          title: '新闻2的标题 1111',
          description: '搜索结果1正文可使用按钮触发下拉菜单。\
                        \设置 split-button 属性来让触发下拉元素呈现为按钮组，左边是功能按钮，右边是触发下拉菜单的按钮，设置为 true 即可。 如果你想要在第三和第四个选项之间添加一个分隔符，你只需要为第四个选项添加一个 divider 的 CSS class。',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel3,
          title: '新闻3的标题 1111',
          description: '搜索结果1正文可使用按钮触发下拉菜单。\
                        \设置 split-button 属性来让触发下拉元素呈现为按钮组，左边是功能按钮，右边是触发下拉菜单的按钮，设置为 true 即可。 如果你想要在第三和第四个选项之间添加一个分隔符，你只需要为第四个选项添加一个 divider 的 CSS class。',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel4,
          title: '新闻4的标题 1111',
          description: '搜索结果1正文可使用按钮触发下拉菜单。\
                        \设置 split-button 属性来让触发下拉元素呈现为按钮组，左边是功能按钮，右边是触发下拉菜单的按钮，设置为 true 即可。 如果你想要在第三和第四个选项之间添加一个分隔符，你只需要为第四个选项添加一个 divider 的 CSS class。',
          link: 'https://element-plus.org/'
        },
      ],
      searchVideoResults: [    //搜索结果
        {
          image: carousel1,
          title: '新闻1的标题 1111sffsfsfsffsfasfasf',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel2,
          title: '新闻2的标题 1111dgdgsgsgsg',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel3,
          title: '新闻3的标题 1111bbdsgsgsgsgsg',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel1,
          title: '新闻4的标题 1111sgszgsgszg',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel2,
          title: '新闻5的标题 1111sgzsgg',
          link: 'https://element-plus.org/'
        },
        {
          image: carousel3,
          title: '新闻6的标题 1111dxhdfhdfhdhdhd',
          link: 'https://element-plus.org/'
        },
      ],

      // // 以下为选中联赛后显示页面所需数据
      // carouselLeagueItems:[   //联赛页面走马灯
      //   { image: carousel1, description: '图片1的描述', link: 'https://element-plus.org/' },
      //   { image: carousel2, description: '图片2的描述', link: 'https://element-plus.org/' },
      //   { image: carousel3, description: '图片3的描述', link: 'https://element-plus.org/' }
      // ],
      // videoLeagueItems:[
      //   { image: carousel1, description: '图片1的描述', link: 'https://element-plus.org/' },
      //   { image: carousel2, description: '图片2的描述', link: 'https://element-plus.org/' },
      //   { image: carousel3, description: '图片3的描述', link: 'https://element-plus.org/' },
      //   { image: carousel4, description: '图片4的描述', link: 'https://element-plus.org/' },
      //   { image: carousel2, description: '图片5的描述', link: 'https://element-plus.org/' },
      //   { image: carousel3, description: '图片6的描述', link: 'https://element-plus.org/' }
      // ],

      //以下为主页面所需数据
      carouselRecommendItems: [    //推荐模块走马灯显示图片及内容
        { image: carousel1, description: '图片1的描述', link: 'https://element-plus.org/' },
        { image: carousel2, description: '图片2的描述', link: 'https://element-plus.org/' },
        { image: carousel3, description: '图片3的描述', link: 'https://element-plus.org/' }
      ],
      carouselVideoItems: [    //热门视频走马灯显示图片及内容
        { image: carousel1, description: '图片1的描述', link: 'https://element-plus.org/' },
        { image: carousel2, description: '图片2的描述', link: 'https://element-plus.org/' },
        { image: carousel3, description: '图片3的描述', link: 'https://element-plus.org/' }
      ],
      carouselGossipItems: [    //八卦走马灯显示图片及内容
        { image: carousel1, description: '图片1的描述', link: 'https://element-plus.org/' },
        { image: carousel2, description: '图片2的描述', link: 'https://element-plus.org/' },
        { image: carousel3, description: '图片3的描述', link: 'https://element-plus.org/' }
      ],
      recommendItems1: [   //推荐模块展示内容（配图）
        { image: carousel1, description: '图片1的描述123455、888841111', link: 'https://element-plus.org/' },
        { image: carousel2, description: '图片2的描述45.22244324455..54', link: 'https://element-plus.org/' },
      ],
      recommendItems2: [   //推荐模块展示内容（不配图）
        { image: carousel1, description: 'wyh小学期结束后瞬移线虫   |   该杀该打', link: 'https://element-plus.org/' },
        { image: carousel2, description: 'zyp白天叫不应唤不醒,晚上通宵在线   |   猪中猪', link: 'https://element-plus.org/' },
      ],
      videoItems1: [   //视频模块展示内容（配图）
        { image: carousel1, description: '图片1的描述', link: 'https://www.bilibili.com/video/BV1gV411j7ds/?spm_id_from=333.337.search-card.all.click&vd_source=5e89100060575d1aa0566662cd25f57f' },
        { image: carousel2, description: '图片2的描述', link: 'https://www.bilibili.com/video/BV1gV411j7ds/?spm_id_from=333.337.search-card.all.click&vd_source=5e89100060575d1aa0566662cd25f57f' },
      ],
      videoItems2: [   //视频模块展示内容（不配图）
        { image: carousel1, description: 'wyh和五个妹妹一起玩剧本杀', link: 'https://www.bilibili.com/video/BV1gV411j7ds/?spm_id_from=333.337.search-card.all.click&vd_source=5e89100060575d1aa0566662cd25f57f' },
        { image: carousel2, description: 'lth留守301沉淀，期待他成神归来', link: 'https://www.bilibili.com/video/BV1gV411j7ds/?spm_id_from=333.337.search-card.all.click&vd_source=5e89100060575d1aa0566662cd25f57f' },
        { image: carousel3, description: 'zyp二刺螈成分爆发，通宵看碧蓝', link: 'https://www.bilibili.com/video/BV1gV411j7ds/?spm_id_from=333.337.search-card.all.click&vd_source=5e89100060575d1aa0566662cd25f57f' },
        { image: carousel4, description: 'lll在东，疑似在东京，抓到罕见', link: 'https://www.bilibili.com/video/BV1gV411j7ds/?spm_id_from=333.337.search-card.all.click&vd_source=5e89100060575d1aa0566662cd25f57f' },
        { image: carousel4, description: '小孩梓疑似被举办，好似，开香槟', link: 'https://www.bilibili.com/video/BV1gV411j7ds/?spm_id_from=333.337.search-card.all.click&vd_source=5e89100060575d1aa0566662cd25f57f' },
      ],
      gossipItems1: [   //八卦模块展示内容（配图）
        { image: carousel1, description: '图片1的描述sfsdfsfsfasfasfasfsfsv', link: 'https://element-plus.org/' },
        { image: carousel2, description: '图片2的描述xvxdbdxbxbfxbdbvdxvdgszvgdvz', link: 'https://element-plus.org/' },
        { image: carousel3, description: '图片3的描述svdbcb cnvncvb vcncvncvncvncv', link: 'https://element-plus.org/' },
      ],
      gossipItems2: [   //八卦模块展示内容（不配图）
        { image: carousel2, description: 'wyh线虫记——远赴上海市区打炮', link: 'https://element-plus.org/' },
        { image: carousel3, description: 'wyh线虫记——闪现南京剧本杀', link: 'https://element-plus.org/' },
        { image: carousel4, description: 'wyh线虫记——奔赴婚礼现场', link: 'https://element-plus.org/' },
        { image: carousel4, description: 'wyh线虫记——压力队友，刀了', link: 'https://element-plus.org/' },
        { image: carousel4, description: 'wyh线虫记——晖神carrry带你飞', link: 'https://element-plus.org/' },
      ],
      ChinaItems: [   //中超模块展示内容
        { image: carousel1, description: '为何wh的小丑属性愈发变强', link: 'https://element-plus.org/' },
        { image: carousel2, description: 'wh是否能在yh的阴影下认识新的女生', link: 'https://element-plus.org/' },
        { image: carousel3, description: '论wh的腹肌什么时候会变为八块', link: 'https://element-plus.org/' },
        { image: carousel4, description: 'wh这么喜欢科比是因为没有螺旋桨吗', link: 'https://element-plus.org/' },
      ],
      ENGLANDItems: [   //英超模块展示内容
        { image: carousel1, description: '同济大学吧小孩梓横空出世', link: 'https://element-plus.org/' },
        { image: carousel2, description: '逆天小孩梓是否是ky人？', link: 'https://element-plus.org/' },
        { image: carousel3, description: '其实都是玩原神玩的', link: 'https://element-plus.org/' },
        { image: carousel4, description: '唉,v87死了得了', link: 'https://element-plus.org/' },
      ],
      FranceItems: [   //法甲模块展示内容
        { image: carousel1, description: '图片1的描述', link: 'https://element-plus.org/' },
        { image: carousel2, description: '图片2的描述', link: 'https://element-plus.org/' },
        { image: carousel3, description: '图片3的描述', link: 'https://element-plus.org/' },
        { image: carousel4, description: '图片4的描述', link: 'https://element-plus.org/' },
      ],
      ItalyItems: [   //意甲模块展示内容
        { image: carousel1, description: '图片1的描述', link: 'https://element-plus.org/' },
        { image: carousel2, description: '图片2的描述', link: 'https://element-plus.org/' },
        { image: carousel3, description: '图片3的描述', link: 'https://element-plus.org/' },
        { image: carousel4, description: '图片4的描述', link: 'https://element-plus.org/' },
      ],
      GermanyItems: [   //德甲模块展示内容
        { image: carousel1, description: '7.16日，出勤', link: 'https://element-plus.org/' },
        { image: carousel2, description: '7.17日，出勤', link: 'https://element-plus.org/' },
        { image: carousel3, description: 'lll你怎能如此堕落', link: 'https://element-plus.org/' },
        { image: carousel4, description: '7.19日，出勤', link: 'https://element-plus.org/' },
      ],
      SpainItems: [   //西甲模块展示内容
        { image: carousel1, description: '走进夏天一周不洗澡的真相', link: 'https://element-plus.org/' },
        { image: carousel2, description: '图泛黄的被子背后隐藏了什么', link: 'https://element-plus.org/' },
        { image: carousel3, description: '其实这个才是真OP', link: 'https://element-plus.org/' },
        { image: carousel4, description: '究竟是人性的泯灭还是道德的沦丧', link: 'https://element-plus.org/' },
      ],
    }
  },
  mounted() {
    window.addEventListener('scroll', this.handleScroll); // 监听滚动事件
  },
  beforeUnmount() {
    window.removeEventListener('scroll', this.handleScroll); // 取消监听滚动事件
  },
  methods: {
    search() {
      this.activeTab = 'Search';
      // 执行搜索逻辑，这里假设是一个简单的前端搜索
      // 可以根据需求修改为发送异步请求到服务器进行搜索
      this.searchResults = this.performSearch(this.searchInput);

    },
    performSearch(keyword) {
      // 在这里执行实际的搜索逻辑，返回搜索结果的数组
      // 这里只是一个示例，实际应用中可以根据需求调用接口或者处理数据
      const results = this.searchResults;
      // 这里假设搜索结果是根据关键字匹配过滤的
      return results.filter(result => result.title.includes(keyword));
    },
    handleScroll() {
      const scrollThreshold = 200;
      const scrollTop = window.scrollY;

      this.isFixed = scrollTop > scrollThreshold;
    },
    //打开链接的页面
    openLink(url) {
      window.open(url, '_blank');
    },
    //跳转到八卦页面
    openGossipMore() {
      ;
    },
    //跳转到视频页面
    openVideoMore() {
      ;
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
  width: 100%;
  height: 100%;
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
  width: 110%;
  height: 110%;
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
  width: 40%;
  height: 40%;
  flex-shrink: 0;
  color: #000;
  font-family: Mogra;
  font-size: 14px;
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
  width: 100%;
  height: 100%;
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
  width: 120%;
  height: 120%;
  position: relative;
}

/* 标题样式 */
.titleSearch {
  font-weight: bold;
  width: 50%;
  /* 设置容器的宽度 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
  white-space: nowrap;
  /* white-space: nowrap;  不换行 */
  margin-top: 5px;
  position: relative;
  left: 8%;
}

/* 描述样式 */
.descriptionSearch {
  width: 60%;
  /* 设置容器的宽度 */
  max-height: 120px;
  /* 设置最大高度 */
  overflow: hidden;
  /* 隐藏超出容器宽度的文本 */
  text-overflow: ellipsis;
  /* 使用省略号表示超出的文本 */
  margin-top: 5px;
  position: relative;
  left: 8%;
}

/* <--新闻搜索结果结束--> */

/* <--视频搜索结果--> */
.iconSearch {
  font-size: 23px;
  position: relative;
  top: -10vh;
  left: 90%;
}

.imgVideoSearch {
  top: 0vh;
  left: -2%;
  width: 90%;
  height: 90%;
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
  left: -20%;
  top: 0vh;
  width: 180%;
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
  top: -11vh;
  left: -0.6vh;
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
  background-color: lightblue;
  height:100vh;
}

.rightLeague {
  flex: 1;
  background-color: lightyellow;
  position: relative; /* 设置为相对定位，以便内部元素使用绝对定位 */
  height:30vh;
}

.fixedLeague {
  position: fixed; /* 设置为固定定位 */
  top: 0; /* 固定在页面顶部 */
  right: 0; /* 固定在页面右侧 */
}

.floating-block {
  position: absolute; /* 设置为绝对定位 */
  top: 50px; /* 设置悬浮块的初始位置 */
  right: 10px; /* 设置悬浮块的初始位置 */
  background-color: lightgreen;
}

.remaining-content {
  margin-top: 100px; /* 为了避免悬浮块覆盖内容，设置内容的上边距 */
  height:20vh;
}
/* <--<--联赛样式结束-->--> */
</style>