<template>
    <div class="follow-list">
        <div class="follow-container">
            <el-card class="follow-card" v-for="(user, index) in followList" :key="index">
                <div class="user-info">
                    <img :src="user.avatar" alt="User Avatar" class="avatar" />
                    <div class="user-details">
                        <p class="username">{{ user.username }}</p>
                        <p class="bio">{{ user.bio }}</p>
                    </div>
                </div>
                <div class="follow-button">
                    <el-button type="primary" size="small">关注</el-button>
                </div>
            </el-card>
        </div>
    </div>
</template>
  
<script>
import axios from 'axios';
export default {
    data() {
        return {
            followList: []  // 用户数据...
        };
    },
    mounted() {
        this.getfollowList();
    },
    methods: {
        async getfollowList() {
            const token = localStorage.getItem('token');
            let response
            try {
                const headers = {
                    Authorization: `Bearer ${token}`,
                };
                response = await axios.post('/api/User/following', {}, { headers });
            } catch (err) {
                console.log(err);
            }
            console.log(response)
        }
    }
};
</script>
  
<style scoped>
.follow-list {
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
}

.follow-container {
    width: 100%;
    max-height: 720px;
    /* 设置最大高度 */
    overflow-y: auto;
    /* 添加垂直滚动条 */
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
}

.follow-container::-webkit-scrollbar {
    width: 5px;
    /* 设置滚动条宽度 */
}

.follow-container::-webkit-scrollbar-thumb {
    background-color: #888;
    /* 设置滚动条颜色 */
    border-radius: 5px;
    /* 设置滚动条圆角 */
}

.follow-card {
    width: calc(33.33% - 20px);
    /* Three cards per row with some gap */
}

.user-info {
    display: flex;
    align-items: center;
}

.avatar {
    width: 50px;
    height: 50px;
    border-radius: 50%;
    margin-right: 10px;
}

.user-details {
    flex: 1;
}

.username {
    font-weight: bold;
}

.bio {
    color: #666;
}

.follow-button {
    text-align: center;
    margin-top: 10px;
}
</style>
