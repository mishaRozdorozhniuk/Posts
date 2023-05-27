<template>
  <div>
    <h2>Page with posts</h2>
    <my-input
        :model-value="searchQuery"
        @update:model-value="setSearchQuery"
        placeholder="Search..."
    />
    <div class="app__btns">
      <my-button @click="showDialog">
        Create Post
      </my-button>
      <my-select
          :model-value="selectedSort"
          @update:model-value="setSelectedSort"
          :options="sortOptions"
      />
    </div>
    <my-dialog v-model:show="dialogVisible">
      <post-form
          @create="createPost"
      />
    </my-dialog>
    <post-list
        :posts="sortedAndSearchedPosts "
        @remove="removePost"
        v-if="!isPostLoading"
    />
    <div v-else>Loading...</div>
    <post-paginations
        :page="page"
        :total-pages="totalPages"
        :change-page="changePage" />
  </div>
</template>

<script>
import PostList from '@/components/PostList'
import PostForm from '@/components/PostForm'
import MyDialog from "@/components/UI/MyDialog";
import MyButton from "@/components/UI/MyButton";
import MySelect from "@/components/UI/MySelect";
import MyInput from "@/components/UI/MyInput";
import PostPaginations from "@/components/PostPaginations";
import { mapActions, mapMutations, mapGetters, mapState} from 'vuex'

export default {
  components: {
    PostPaginations,
    MyInput,
    MySelect,
    MyButton,
    MyDialog,
    PostList,
    PostForm
  },
  data() {
    return {
      dialogVisible: false,
    }
  },
  methods: {
    ...mapMutations({
      setPage: 'post/setPage',
      setSearchQuery: 'post/setSearchQuery',
      setSelectedSort: 'post/setSelectedSort'
    }),
    ...mapActions({
      fetchPosts: 'post/fetchPosts',
      addPost: "post/addPost",
      deletePost: "post/deletePost"
    }),
    ...mapMutations({
      setTitle: 'post/setTitle',
      setBody: 'post/setBody',
    }),
    async createPost(post) {
      this.setTitle(post.title)
      this.setBody(post.body)
      await this.addPost()
      this.dialogVisible = false
    },
    removePost(post) {
      this.deletePost(post.gid)
    },
    showDialog() {
      this.dialogVisible = true
    },
    changePage(pageNumber) {
      this.page = pageNumber
    },
  },
  mounted() {
    this.fetchPosts()
  },
  computed: {
    ...mapState({
      posts: state => state.post.posts,
      searchQuery: state => state.post.searchQuery,
      dialogVisible: state => state.post.dialogVisible,
      isPostLoading: state => state.post.isPostLoading,
      selectedSort: state => state.post.selectedSort,
      page: state => state.post.page,
      limit: state => state.post.limit,
      totalPages: state => state.post.totalPages,
      sortOptions: state => state.post.sortOptions
    }),
    ...mapGetters({
      sortedPosts: 'post/sortedPosts',
      sortedAndSearchedPosts: "post/sortedAndSearchedPosts",
      getUserGid: "post/getUserGid"
    }),
  },
  watch: {
    page() {
      this.fetchPosts()
    }
  }
}
</script>

<style>
.app__btns {
  display: flex;
  justify-content: space-between;
  margin: 20px;
}

form {
  display: flex;
  flex-direction: column;
}

</style>