<template>
  <my-dialog v-model:show="dialogVisible">
    <h1 class="sign-in-title">Sign in</h1>
    <form class="sign-in" @submit.prevent="handleSubmitForm">
      <input
          type="text"
          placeholder="First name"
          v-model="firstName">
      <input
          type="text"
          placeholder="Last name"
          v-model="lastName">
      <button class="sign-in-button">Sign In</button>
    </form>
  </my-dialog>
</template>

<script>
import MyDialog from "@/components/UI/MyDialog";
import {mapActions, mapGetters, mapMutations} from 'vuex'

export default {
  components: {MyDialog},
  data() {
    return {
      dialogVisible: true,
      firstName: '',
      lastName: ''
    }
  },
  methods: {
    ...mapActions({
      loginUser: 'post/loginUser'
    }),
    ...mapGetters({
      getIsAuth: 'post/getIsAuth'
    }),
    ...mapMutations({
      setFirstName: 'post/setFirstName',
      setLastName: 'post/setLastName'
    }),
    async handleSubmitForm() {
      this.setFirstName(this.firstName)
      this.setLastName(this.lastName)
      await this.loginUser()
      if(this.getIsAuth) {
        this.$router.push('/posts')
      }
    }
  }
};
</script>

<style scoped>
.sign-in-title {
  text-align: center;
  font-size: 20px;
}
.sign-in-button {
  cursor: pointer;
  background: #1c1c1c;
  border: none;
  padding: 8px;
  color: #fff;
  max-width: 100px;
  margin: 0 auto;
  border-radius: 5px;
}
.sign-in input {
  padding: 10px;
  margin: 10px;
  border-radius: 5px;
  border: 1px solid #b2b2b2;
}
</style>
