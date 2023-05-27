<template>
  <form @submit.prevent>
    <h4>Create Post</h4>
    <my-input
        v-model="post.title"
        type="text"
        placeholder="Name"/>
    <my-input
        v-model="post.body"
        type="text"
        placeholder="Description"/>
    <my-button class="btn-create" @click="createPost">Create</my-button>
  </form>
</template>

<script>
import MyButton from '@/components/UI/MyButton'
import MyInput from "@/components/UI/MyInput";

export default {
  components: {
    MyInput,
    MyButton
  },
    data() {
      return {
        post: {
          title: '',
          body: ''
        }
      }
    },
    methods: {
      createPost() {
        this.post.id = Date.now()
        this.$emit('create', this.post)
        this.post = {
          title: '',
          body: ''
        }
      }
    },
  }
</script>

<style scoped>
.btn-create {
  margin-top: 15px;
}
</style>