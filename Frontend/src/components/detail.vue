<script>
import MyNav from './nav.vue';
export default {
    components:{
        'my-nav': MyNav
    },
    data(){
        return{
            uId:112233,
            tittle:"关于水神机制的猜测",
            uImg:"这是头像",
            uName:"仙贝牌辣条",
            uText:"顺便小庆祝一手猜对了水神的性格，就要乐子人捏。水神作为枫丹的领导人，其必然会影响到自身国家前期角色的机制。类似的例子就比如钟离有强效护盾所以璃月3c都刚需生存辅助；雷神有强力充能所以稻妻或者说2.0时代的角色无论能不能抱雷神的大腿都会有巨大的充能需求，要么绑雷神要么带西风、祭礼等充能道具；还有最重量级的须弥，就差把“不抽草神不配玩须弥”写在脸上了由此，我们将视角转向包括水主在内已经“公布”技能机制的枫丹新角色。只从定性分析，除了菲米尼是物理体系，包括水主在内的三个枫丹角色都体现出了高生命值消耗的特性，如果水神要对他们有明确增幅就需要有提高生存能力的机制。首先排除护盾然后将视角转向奶妈的路数，水神如果要恶心一手需要压血线的老角色的同时又保证枫丹新角色不会因掉血而坑死自己，只需要加一句“损失生命值越多回复生命值越高。”从场外因素来说，这既保证了水神即使脱离枫丹体系也能有自己的作用，又堵上了玩家质疑的声音",
            date:"2023-07-07",
            judgers:[
                {jId:1,jName:"草神和伞兵什么时候结婚",jText:"你说得对，但是原神是一款",jDate:"2023-07-08"},
                {jId:2,jName:"丁真Official",jText:"义！乌！",jDate:"2023-07-09"},
                {jId:3,jName:"杰子",jText:"同学报一下学号姓名给你加创新学分",jDate:"2023-07-10"},
            ],

        }
    }
    
}
</script>

<template>
    <div class="common-layout">
        <my-nav/>
        <el-container class="post-container">
            <el-header>
                <text class="header-text">{{ tittle }}</text>
                <el-button class="header-respond-btn" style="right:7vw">收藏</el-button>
                <el-button class="header-respond-btn">回复</el-button>
            </el-header>
            <el-container style="border: 1px solid #ccc;">
                <el-aside>
                    <!-- <img class="rooter-img" src=""> -->
                    <div class="rooter-img">{{ uImg }}</div>
                    <div class="rooter-name">
                        <p class="rooter-name-typography">{{ uName }}</p>
                    </div>
                </el-aside>
                <el-main>
                    <!-- 主帖 -->
                    <div class="rooter-post">
                        {{ uText }}
                        <div class="bottom-detail">
                            <div>{{ date }}</div>
                        </div>
                    </div>
                    <!-- 评论 -->
                    <div class="judger-post">
                        <div v-for="judger in judgers" :key="judger.jId"> 
                            <el-divider style="color: black;height:2vw"></el-divider>
                            <p><text style="color: rgb(24, 151, 235);">{{ judger.jName }} ：</text><text>{{ judger.jText }}</text></p>
                            <p style="top:2vw">{{ judger.jDate }}</p>
                        </div>
                    </div>
                </el-main>
            </el-container>
        </el-container>
        <div class="input-respond-container">
            <div style="margin: 20px 0">
                <el-input
                    v-model="respond"
                    :rows="5"
                    type="textarea"
                    placeholder="和大家分享你的看法~"
                />
                <el-button class="input-respond-btn">发布评论</el-button>
            </div>
        </div>
    </div>
</template>

<style scoped>
/*帖子的整体容器*/
.post-container{
    width:70vw;
    height:80vw;
	background: #eee;
	position: absolute;
	left: 20%;top: 30%;
	margin-left: -5vw;
	margin-top: -5vw;
}
/*头部帖子标题+收藏/回复按钮*/
.el-header{
    text-align: left;
}
.header-text{
    font-family: SimHei;
    font-size: 1.5rem;
    font-weight: 200;
    position: absolute;
    top:1vw;
    left: 2vw;
}
.header-respond-btn{
    text-align: center;
    background-color: aliceblue;
    position: absolute;
    top:1vw;
    right: 2vw;
}
/*左侧楼主信息栏*/
.el-aside{
    width:15vw;
    background-color: rgb(230, 230, 230);
    text-align: center;
}
/*楼主信息 头像+昵称*/
.rooter-img{
    position: relative;
    width:6vw;
    height:6vw;
    top:2vw;
    bottom:2vw;
    left:4vw;
    background-color: bisque;
}
.rooter-name{
    position: relative;
    width:7vw;
    height:7vw;
    top:2.5vw;
    bottom:2vw;
    left:3.5vw;
}
.rooter-name-typography{
    font-family: KaiTi;
    font-size:1.25rem;
    color:rgb(41, 93, 151);
}
/*主信息板块*/
.el-main{
    text-align: left;
    padding-left: 3vw;
    padding-right: 3vw;
}
.bottom-detail{
    width:100%;
    height:10vw;
    position: relative;
    top:3vw;
    bottom: 0;
    right:0vw;
}
.input-respond-container{
    background-color: rgb(251, 249, 246);
    position: fixed;
    bottom: 0;
    width:70vw;
    height:13vw;
    left: 20%;
	margin-left: -5vw;
}
.input-respond-btn{
    text-align: center;
    background-color:#6fc8e3;
    position: relative;
    margin-top:1vw;
    right:-60vw;
}
</style>
