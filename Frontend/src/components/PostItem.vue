<template>
  <div class="post">
    <div>
      <div>{{post.id }}</div>
      <strong>Name: {{post.title}}}</strong>
      <p>Description: {{post.body}}</p>
    </div>
    <div class="post__btns">
      <my-button @click="$router.push(`/posts/${post.id}`)">Open</my-button>
      <my-button @click="$emit('remove', post)">Delete</my-button>
    </div>
  </div>
</template>

<script>
  export default {
    props: {
      post: {
        type: Object,
        required: true
      }
    }
  }
</script>

<style scoped>
  .post {
    padding: 15px;
    /*border: 2px solid teal;*/
    box-shadow: 0 7px 33px -15px rgba(0,0,0,0.75);
    border: 1px solid #000;
    margin: 10px;
    border-radius: 15px;
    display: flex;
    align-items: center;
    justify-content: space-between;
  }
  .post__btns {
    display: flex;
  }

  .post__btns button {
    margin-right: 5px;
  }
</style>