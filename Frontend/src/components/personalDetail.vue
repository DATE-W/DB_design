<template>
    <!-- 中间一列 -->

    <!-- 主标题：个人主页 -->
    <div class="titleLayout" style="left:22vw;top:5vh">
        <p class="titleh1Typo">个人主页</p>
    </div>

    <!-- 中间列标题：动态 -->
    <div class="titleLayout" style="left:22vw;bottom:62vh">
        <p class="titleTypo">动态</p>
    </div>

    <!-- 中间列内容 -->

    <!-- 收藏夹 -->
    <border-box class="bBox" style="width: 800px;height: 90px;  bottom: 50vh;left:21vw" @click="changeF">
        <!-- 收藏图标 -->
        <border-box class="iconBox">
            <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
                <circle cx="32" cy="32" r="32" fill="#FFDFD6" />
            </svg>

            <border-box class="miniBox">
                <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path
                        d="M23.1513 28.8945C22.9914 28.8976 22.8334 28.8572 22.6937 28.7773L16.0126 25.2605L10.1322 28.4256C9.16188 28.9546 7.95673 28.5831 7.43231 27.5933C7.22826 27.1938 7.15601 26.7372 7.22639 26.2921L8.37042 19.5399L3.58836 14.6985C3.21179 14.3155 3 13.7944 3 13.2508C3 12.7071 3.21179 12.186 3.58836 11.803C3.89484 11.4818 4.29773 11.2754 4.73239 11.2169L11.2877 10.2205L14.205 4.14821C14.5381 3.44585 15.2333 3 15.9954 3C16.7575 3 17.4527 3.44585 17.7858 4.14821L20.7145 10.2439L27.2927 11.2521C27.8213 11.33 28.2971 11.6222 28.6133 12.063C28.9296 12.5039 29.0597 13.0565 28.9745 13.5966C28.9124 14.0404 28.7117 14.4517 28.4025 14.7688L23.6433 19.5516L24.3068 23.1856C24.3966 23.7387 24.0338 24.2624 23.4945 24.3579C22.9645 24.4581 22.4544 24.1027 22.3505 23.5607L21.6755 19.9268C21.5629 19.258 21.7765 18.5751 22.2476 18.098L27.0182 13.2801L20.44 12.2602C19.7783 12.1561 19.2137 11.7145 18.9413 11.088L16.0126 5.01568L13.0495 11.1817C12.7771 11.8083 12.2125 12.2499 11.5508 12.354L4.99552 13.3504L9.73182 18.1566C10.2008 18.6253 10.4147 19.3004 10.3038 19.9619L9.15981 26.7141L15.0401 23.5607C15.6247 23.2334 16.3317 23.2334 16.9164 23.5607L23.5861 27.0775C23.9903 27.2953 24.198 27.7671 24.0898 28.2218C23.9815 28.6765 23.5852 28.9973 23.1285 29L23.1513 28.8945Z"
                        fill="#161616" />
                </svg>
            </border-box>
        </border-box>

        <!-- 收藏提示语 -->
        <border-box class="textBox1">
            <h3 class="textTypo4">收藏夹</h3>
        </border-box>
        <border-box class="textBox2">
            <p3 class="textTypo3">您近期收藏了 《王晗天天拉屎》、《lll今天又睡过头了》 等5篇 媒体文章</p3>
        </border-box>


    </border-box>


    <!-- 站内信 -->
    <border-box class="bBox" style="width: 800px;height: 90px;  bottom: 35vh;left:21vw" @click='changeN'>
        <!-- 站内信图标 -->
        <border-box class="iconBox">
            <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
                <circle cx="32" cy="32" r="32" fill="#52C3FF" />
            </svg>

            <border-box class="miniBox">
                <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path fill-rule="evenodd" clip-rule="evenodd"
                        d="M26.1274 15.0412C26.6528 15.8317 26.9542 16.7476 27 17.693L26.9656 17.932C27.0051 19.2259 26.5731 20.4906 25.7485 21.4943C24.6955 22.63 23.256 23.3394 21.7071 23.486C17.8916 23.9047 14.0411 23.9047 10.2256 23.486C8.69929 23.3294 7.28485 22.6202 6.25306 21.4943C5.41491 20.505 4.96991 19.2464 5.00158 17.9548V17.7955C5.06471 16.8217 5.3898 15.8825 5.94306 15.0754L6.03491 14.9616C6.61029 14.3144 7.00596 13.5299 7.18305 12.6853V11.7066V10.3408C7.34586 9.92967 7.7663 9.67755 8.20927 9.72545C8.65223 9.77335 9.00794 10.1094 9.07749 10.5457V11.6838V12.8902C9.08195 12.9318 9.08195 12.9738 9.07749 13.0154C8.83843 14.1895 8.292 15.2807 7.49305 16.1793C7.18537 16.6652 7.01122 17.2223 6.98787 17.7955V18.0459C6.96238 18.852 7.23148 19.6401 7.74564 20.2652C8.45849 20.995 9.41173 21.4473 10.4323 21.5399C14.1261 21.9382 17.8525 21.9382 21.5463 21.5399C22.5948 21.4445 23.5713 20.9706 24.2904 20.2083C24.7793 19.598 25.0354 18.8363 25.0137 18.0572V17.7955C24.9902 17.2204 24.8203 16.6605 24.52 16.168C23.6878 15.2795 23.1092 14.1875 22.8437 13.004C22.8393 12.9624 22.8393 12.9204 22.8437 12.8788V11.661V10.5229C22.5108 7.08583 19.0549 4.95755 16.0467 4.95755C14.7678 4.95464 13.51 5.28008 12.3956 5.90219C12.0871 6.08605 11.702 6.08888 11.3908 5.90956C11.0795 5.73025 10.8914 5.39722 10.8998 5.04045C10.9082 4.68368 11.1118 4.35972 11.4312 4.19502C12.812 3.42176 14.369 3.01045 15.9549 3C19.9274 3 24.3822 5.79976 24.8874 10.2953V11.6383V12.6398C25.0599 13.486 25.4561 14.2716 26.0356 14.916C26.0699 14.955 26.1006 14.9969 26.1274 15.0412ZM16.3866 26.96C17.0711 26.8591 17.6863 26.494 18.0956 25.9459H18.1072C18.461 25.5258 19.0947 25.4669 19.5225 25.8143C19.9503 26.1618 20.0103 26.784 19.6564 27.2041C18.7842 28.3242 17.4347 28.9868 15.9989 29C14.5672 28.9833 13.2227 28.3211 12.353 27.2041C11.9992 26.784 12.0591 26.1618 12.487 25.8143C12.9148 25.4669 13.5484 25.5258 13.9022 25.9459C14.0576 26.1499 14.242 26.3309 14.4497 26.4835C15.0049 26.8894 15.7021 27.0609 16.3866 26.96Z"
                        fill="white" />
                </svg>
            </border-box>
        </border-box>

        <!-- 站内信提示语 -->
        <border-box class="textBox1">
            <h3 class="textTypo4">站内信</h3>
        </border-box>
        <border-box class="textBox2">
            <p3 class="textTypo3">Admin_LLL：尊敬的用户，您好。本站近日发现有部分用户发帖造谣，声称某管理员是🐖，本站声明……</p3>
        </border-box>

    </border-box>


    <!-- 积分钱包 -->
    <border-box class="bBox" style="width: 800px;height: 90px;  bottom: 20vh;left:21vw" @click="changeC">
        <!-- 积分钱包图标 -->
        <border-box class="iconBox">
            <svg width="64" height="64" viewBox="0 0 64 64" fill="none" xmlns="http://www.w3.org/2000/svg">
                <circle cx="32" cy="32" r="32" fill="#FFCB57" />
            </svg>

            <border-box class="miniBox">
                <svg width="32" height="32" viewBox="0 0 32 32" fill="none" xmlns="http://www.w3.org/2000/svg">
                    <path fill-rule="evenodd" clip-rule="evenodd"
                        d="M10.5204 4H21.4785C25.6258 4 29 7.49313 29 11.788V21.212C29 25.5069 25.6258 29 21.4785 29C21.0001 29 20.6118 28.5979 20.6118 28.1025C20.6118 27.6071 21.0001 27.205 21.4785 27.205C24.6701 27.205 27.2667 24.5172 27.2667 21.212V13.6908H22.998C21.5964 13.692 20.4535 14.8743 20.4524 16.3271C20.4535 17.7799 21.5964 18.9622 22.998 18.9634H24.7718C25.2502 18.9634 25.6385 19.3655 25.6385 19.8609C25.6385 20.3563 25.2502 20.7584 24.7718 20.7584H22.998C20.6396 20.7572 18.7202 18.7695 18.719 16.3271C18.7202 13.8846 20.6396 11.8969 22.998 11.8957H27.2667V11.788C27.2667 8.48279 24.6701 5.79503 21.4785 5.79503H10.5204C7.94809 5.79503 5.78951 7.55177 5.03724 9.95831H16.4611C16.9395 9.95831 17.3277 10.3604 17.3277 10.8558C17.3277 11.3524 16.9395 11.7533 16.4611 11.7533H4.7368C4.7368 11.7593 4.73593 11.765 4.73507 11.7707C4.7342 11.7764 4.73333 11.7821 4.73333 11.788V21.212C4.73333 24.5172 7.32871 27.205 10.5204 27.205H16.0335C16.5119 27.205 16.9002 27.6071 16.9002 28.1025C16.9002 28.5979 16.5119 29 16.0335 29H10.5204C6.37307 29 3 25.5069 3 21.212V11.788C3 7.49313 6.37307 4 10.5204 4ZM22.3193 16.2462C22.3193 15.7508 22.7075 15.3487 23.1859 15.3487H23.58C24.0584 15.3487 24.4466 15.7508 24.4466 16.2462C24.4466 16.7416 24.0584 17.1437 23.58 17.1437H23.1859C22.7075 17.1437 22.3193 16.7416 22.3193 16.2462Z"
                        fill="#161616" />
                </svg>
            </border-box>
        </border-box>

        <!-- 积分提示语 -->
        <border-box class="textBox1">
            <h3 class="textTypo4">积分</h3>
        </border-box>
        <border-box class="textBox2">
            <p3 class="textTypo3">您的账户里还有 4396 积分 | 状态：兵马充足，快来跟随山泥若博导一起驰骋沙场吧！</p3>
        </border-box>


    </border-box>






    <!-- 右侧一列 -->
    <div class="titleLayout" style="width: 200px;left:78vw;bottom:83vh">
        <p class="titleTypo">关注</p>
    </div>

    <border-box class="bBox" style="width: 300px;height: 120px; bottom: 67vh;right:3vw">
        <border-box class="bBox" style="width: 48px;height:48px;top: 1vh;left:1vw">
            <!-- 待写入跳转逻辑 -->
            <img src="../assets/img/carousel4.png" class="imgUser">
        </border-box>

        <!-- 关注的人的ID -->
        <border-box class="textBox3">
            <p3 class="textTypo5">曼城足球俱乐部</p3>
        </border-box>
        <!-- 发帖时间 -->
        <border-box class="textBox4">
            <p3 class="textTypo6">32分钟 前</p3>
        </border-box>
        <!-- 帖子内容预览 -->
        <border-box class="textBox5">
            <p3 class="textTypo1">我们是冠军！！！</p3>
        </border-box>

    </border-box>

    <border-box class="bBox" style="width: 300px;height: 120px; bottom: 47vh;right:3vw">
        <border-box class="bBox" style="width: 48px;height:48px;top: 1vh;left:1vw">
            <!-- 待写入跳转逻辑 -->
            <img src="../assets/img/shinyruo.png" class="imgUser">
        </border-box>

        <!-- 关注的人的ID -->
        <border-box class="textBox3">
            <p3 class="textTypo5">Dr.Shiny_Ruo</p3>
        </border-box>
        <!-- 发帖时间 -->
        <border-box class="textBox4">
            <p3 class="textTypo6">2小时 前</p3>
        </border-box>
        <!-- 帖子内容预览 -->
        <border-box class="textBox5">
            <p3 class="textTypo1">芜，罗德里，好帅的远射！收米！</p3>
        </border-box>

    </border-box>

    <border-box class="bBox" style="width: 300px;height: 120px; bottom: 27vh;right:3vw">
        <border-box class="bBox" style="width: 48px;height:48px;top: 1vh;left:1vw">
            <!-- 待写入跳转逻辑 -->
            <img src="../assets/img/shinyruo.png" class="imgUser">
        </border-box>

        <!-- 关注的人的ID -->
        <border-box class="textBox3">
            <p3 class="textTypo5">Dr.Shiny_Ruo</p3>
        </border-box>
        <!-- 发帖时间 -->
        <border-box class="textBox4">
            <p3 class="textTypo6">3小时 前</p3>
        </border-box>
        <!-- 帖子内容预览 -->
        <border-box class="textBox5">
            <p3 class="textTypo1">喂喂喂？今天播到五点半，兄弟们可以买好泡面了，这把压力……</p3>
        </border-box>

    </border-box>

    <border-box class="bBox" style="width: 300px;height: 120px; bottom: 7vh;right:3vw">
        <border-box class="bBox" style="width: 48px;height:48px;top: 1vh;left:1vw">
            <!-- 待写入跳转逻辑 -->
            <img src="../assets/img/carousel2.png" class="imgUser">
        </border-box>

        <!-- 关注的人的ID -->
        <border-box class="textBox3">
            <p3 class="textTypo5">皇家马德里</p3>
        </border-box>
        <!-- 发帖时间 -->
        <border-box class="textBox4">
            <p3 class="textTypo6">5月18日 05：25</p3>
        </border-box>
        <!-- 帖子内容预览 -->
        <border-box class="textBox5">
            <p3 class="textTypo1">技不如人，甘拜下风！感谢美凌格一个赛季的支持！我们会再……</p3>
        </border-box>

    </border-box>
</template>

<script>
export default {
    methods: {
        changeF() {
            this.$emit('change-page', 'favorite'); // 触发自定义事件，并传递参数'favorite'
        },
        changeN() {
            this.$emit('change-page', 'notification'); // 触发自定义事件，并传递参数'favorite'
        },
        changeC() {
            this.$emit('change-page', 'credits'); // 触发自定义事件，并传递参数'favorite'
        },
    }
}
</script>

<style scoped>
/* 容器框样式 */
.bBox {
    position: absolute;
    flex-shrink: 0;
    border-radius: 32px;
    border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);
    /* 正式版本用此颜色*/
    background-color: #ffffff;
    /* 方便检测位置*/
    /* background-color: #c3c3c3;  */
}

.cBox {
    position: absolute;
    left: 1rem;
    width: 10.5rem;
    height: 5rem;
    flex-shrink: 0;
    /* 正式版本用此颜色*/
    background-color: #ffffff;
    /* 方便检测位置*/
    /* background-color: #c3c3c3; */
}

.iconBox {
    position: absolute;
    top: 0.8rem;
    left: 2rem;
    width: 4rem;
    height: 4rem;
    flex-shrink: 0;
    /* 仅为检测位置，正式版本删去*/
    /* border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);  */
}

.miniBox {
    position: absolute;
    top: 1rem;
    left: 1rem;
    width: 2rem;
    height: 2rem;
    flex-shrink: 0;
    /* 仅为检测位置，正式版本删去*/
    /* border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);  */
}

.textBox1 {
    position: absolute;
    width: 30rem;
    height: 2rem;
    left: 8rem;
    top: -0.8rem;
}

.textBox2 {
    position: absolute;
    width: 50rem;
    height: 2rem;
    left: 8rem;
    top: 3.2rem;
}

.textBox3 {
    position: absolute;
    width: 10rem;
    height: 2rem;
    left: 5rem;
    top: 0.7rem;
}

.textBox4 {
    position: absolute;
    width: 10rem;
    height: 2rem;
    left: 5rem;
    top: 2.2rem;
}

.textBox5 {
    position: absolute;
    width: 17rem;
    height: 2rem;
    left: 1.2rem;
    top: 3.8rem;
}

/* 按钮样式 */
.btn1 {
    position: absolute;
}

.btn2 {
    position: absolute;
    flex-shrink: 0;
    border-radius: 16px;
    border: 1px solid var(--colors-light-eaeaea-100, #EAEAEA);
    /* 正式版本用此颜色*/
    background-color: #ffffff;
    /* 方便检测位置*/
    /* background-color: #939393;  */
}

/* 文字风格 */
.userNameLayout {
    position: absolute;
    display: flex;
    width: 156px;
    flex-direction: column;
    flex-shrink: 0;
}

.userNameTypo {
    color: var(--colors-text-dark-172239100, #172239);
    font-family: Verdana;
    font-size: 16px;
    font-style: normal;
    font-weight: 600;
    line-height: 24px;
}

.userMailTypo {
    color: var(--colors-text-dark-6-d-6-d-78100, #6D6D78);
    font-family: Verdana;
    font-size: 12px;
    font-style: normal;
    font-weight: 400;
    line-height: 16px;
}

.titleLayout {
    position: absolute;
    display: flex;
    width: 940px;
    flex-direction: column;
    flex-shrink: 0;
}

.titleTypo {
    color: var(--colors-text-dark-172239100, #172239);
    font-family: Verdana;
    font-size: 24px;
    font-style: normal;
    font-weight: 600;
    line-height: 32px;
}

.titleh1Typo {
    color: var(--colors-text-dark-172239100, #172239);
    font-family: Verdana;
    font-size: 40px;
    font-style: normal;
    font-weight: 600;
    line-height: 48px;
}

.textTypo1 {
    color: var(--colors-text-dark-6-d-6-d-78100, #6D6D78);
    /* Text/P2/Regular/24pt-line-height */
    font-family: Verdana;
    font-size: 16px;
    font-style: normal;
    font-weight: 400;
    line-height: 24px;
    /* 150% */
}

.textTypo2 {
    color: var(--colors-text-dark-172239100, #172239);
    position: absolute;
    /* Text/H3/Semi-bold */
    font-family: Verdana;
    font-size: 20px;
    font-style: normal;
    font-weight: 600;
    line-height: 32px;
    /* 160% */
}

.textTypo3 {
    color: var(--colors-text-dark-6-d-6-d-78100, #6D6D78);
    text-align: center;
    /* Text/P3/Regular */
    font-family: Verdana;
    font-size: 0.875rem;
    font-style: normal;
    font-weight: 400;
    line-height: 1.5rem;
    /* 171.429% */
}

.textTypo4 {
    color: var(--colors-text-dark-172239100, #172239);
    /* Text/H3/Semi-bold */
    font-family: Verdana;
    font-size: 1.25rem;
    font-style: normal;
    font-weight: 600;
    line-height: 2rem;
    /* 160% */
}

.textTypo5 {
    /* Text/H3/Semi-bold */
    color: var(--colors-text-dark-172239100, #172239);
    font-family: Verdana;
    font-size: 1.1rem;
    font-style: normal;
    font-weight: 500;
    line-height: 1rem;
}

.textTypo6 {
    color: var(--colors-text-dark-6-d-6-d-78100, #6D6D78);
    text-align: center;
    /* Text/P3/Regular */
    font-family: Verdana;
    font-size: 0.875rem;
    font-style: normal;
    font-weight: 400;
    line-height: 1rem;
    /* 171.429% */
}

/* 按钮风格 */
.goinBtn {
    position: absolute;
    width: 20px;
    height: 20px;
    flex-shrink: 0;
}

/* 图片风格 */
.imgUser {
    width: 48px;
    height: 48px;
    border-radius: 48px;
    object-fit: cover;
}

.imgRuo {
    position: absolute;
    left: 3.5rem;
    width: 90px;
    height: 60px;
    border-radius: 30px;
    object-fit: cover;
}
</style>