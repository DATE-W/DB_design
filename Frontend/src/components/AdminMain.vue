<script>
import AdminReportedUsers from './AdminReportedUsers.vue';
import AdminAnnouncement from './AdminAnnouncement.vue';
import AdminReportedPost from './AdminReportedPost.vue';
import AdminAllUsers from './AdminAllUsers.vue';
import AdminNav from './AdminNav.vue';
import { ElMessage, ElMessageBox } from 'element-plus';
export default {
    components:{
        'admin-reported-users': AdminReportedUsers,
        'admin-announcement': AdminAnnouncement,
        'admin-reported-post':AdminReportedPost,
        'admin-all-users':AdminAllUsers,
        'admin-nav':AdminNav,
    },
    data() {
        return {
           announcement:'',
        };
    },
    methods:{
        clearAnnouncementInput()
        {
            this.announcement='';
        },
        subbmitAnnouncement()
        {

        }
    }
}
</script>

<template>
    <div class="common-layout bkBox">
      <el-container>
        <el-header>
            <admin-nav/>
        </el-header>
        <el-container>
          <el-aside>
            <!--被举报的用户-->
            <admin-reported-users/>
            <!--发布公告-->
            <admin-announcement/>
          </el-aside>
            <el-container>
                <el-main>
                    <admin-all-users/>
                    <admin-reported-post/>
                </el-main>
          </el-container>
        </el-container>
      </el-container>
    </div>
  </template>  

<style scoped>
/*设置整个背景填充为淡灰色*/
.bkBox{
    background: #eee;
    position: absolute;
    height: 100%;
    width:100%;
}

/*左侧两栏*/
.el-aside{ 
    position: absolute;
    left:0;
    width: 35%;
    height:92%;
}

/*右侧*/
.el-main{
    position: absolute;
    right:0;
    width: 65%;
    height:92%;
}
</style>