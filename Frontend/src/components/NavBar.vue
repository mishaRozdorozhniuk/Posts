<template>
  <div class="navbar">
    <div class="navbar__logo" @click="$router.push('/')">Vue 3</div>
    <div class="navbar__btns">
      <my-button
          style="margin-left: 20px"
          @click="$router.push('/about')">About website</my-button>
      <my-button
          style="margin-left: 20px"
          @click="$router.push('/posts')">Store page</my-button>
    </div>
  </div>
</template>

<script>
export default {
};
</script>

<style>
.navbar {
  height: 50px;
  background-color: lightgray;
  box-shadow: 2px 2px 4px gray;
  display: flex;
  align-items: center;
  padding: 0 15px;
}

.navbar__btns {
  margin-left: auto;
}

.navbar__logo {
  color: teal;
  font-weight: bold;
  font-size: 20px;
  cursor: pointer;
}
</style>