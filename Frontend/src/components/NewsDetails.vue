<template>
    <div>
        {{ items }}
    </div>
</template>
  
<script>
export default {
    props: {
        items: {
            type: Object,
            required: true,
        }
    },
    methods: {
        test() {
            console.log(123);
            console.log(items);
        }
    }
};
</script>