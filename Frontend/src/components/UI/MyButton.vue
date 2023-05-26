<template>
  <button @click="$emit('remove', post)" class="btn">
    <slot></slot>
  </button>
</template>

<script>
  export default {
    name: 'my-button'
  }
</script>

<style scoped>
  .btn {
    align-self: flex-end;
    color: teal;
    padding: 10px 15px;
    background-color: transparent;
    border: 1px solid teal;
  }
</style>