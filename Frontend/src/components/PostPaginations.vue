<template>
  <div class="page__wrapper">
    <div
        class="page"
        v-for="pageNumber in totalPages"
        :key="pageNumber"
        :class="{
             'current-page': page === pageNumber
           }"
        @click="changePage(pageNumber)"
    >
      {{pageNumber}}
    </div>
  </div>
</template>

<script>
export default {
  props: {
    page: {
      required: true,
      type: Number
    },
    totalPages: {
      type: Number,
      required: true
    },
    changePage: {
      required: true
    }
  }
};
</script>

<style>

.page__wrapper {
  display: flex;
  margin-top: 15px;
}

.page {
  border: 1px solid #000;
  padding: 10px;
  margin-right: 2px;
}

.current-page {
  border: 2px solid teal;
}
</style>