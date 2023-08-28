<template>
    <div>
        {{ Details.newsbody.matchTag }}
    </div>
    <div>
        {{ Details.newsbody.propertyTag }}
    </div>
    <div>
        {{ Details.newsbody.title }}
    </div>
    <div>
        {{ Details.newsbody.summary }}
    </div>
    <div>
        {{ Details.newsbody.contains }}
    </div>
    <div>
        {{ Details.newsbody.news_id }}
    </div>
    <div>
        {{ Details.newsbody.publishDateTime }}
    </div>
    <div v-for="item in Details.pictureRoutes">
        {{ item }}
    </div>
</template>
  
<script>
export default {
    data() {
        return {
            Details: {
                newsbody: {
                    matchTag: "",
                    propertyTag: "",
                    title: "",
                    summary: "",
                    contains: "",
                    news_id: 0,
                    publishDateTime: '',
                },
                pictureRoutes: [],
            },
        };
    },
    created() {
        const queryString = this.$route.query.data;
        if (queryString) {
            const decodedString = decodeURIComponent(queryString);
            this.Details = JSON.parse(decodedString);
        }
    },
};
</script>