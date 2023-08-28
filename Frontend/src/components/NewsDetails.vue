<template>
    <div>
        {{ items }}
    </div>
</template>
  
<script>
export default {
    // props: {
    //     items: {
    //         type: Object,
    //         required: true,
    //     }
    // },
    data() {
        return {
            items: '',
        };
    },
    created() {
        this.items = this.$route.params.items;
    },
    methods: {
        test() {
            console.log(123);
            console.log(this.items);
        }
    }
};
</script>