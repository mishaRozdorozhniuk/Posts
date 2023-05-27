import axios from "axios";

export const postModule = {
    state: () => ({
        posts: [],
        searchQuery: '',
        dialogVisible: false,
        isPostLoading: false,
        selectedSort: '',
        page: 1,
        limit: 10,
        totalPages: 0,
        sortOptions: [
            {value: 'title', name: 'by name'},
            {value: 'body', name: 'by description'}
        ],
        firstName: '',
        lastName: '',
        userGid: '',
        JWT: '',
        isAuth: false,
        title: '',
        body: ''
    }),
    getters: {
        sortedPosts(state) {
            return [...state.posts].sort((post1, post2) =>  post1[state.selectedSort]?.localeCompare(post2[state .selectedSort]))
        },
        sortedAndSearchedPosts(state, getters) {
            return getters.sortedPosts.filter(post => post.title.toLowerCase().includes(state.searchQuery.toLowerCase()))
        },
        getIsAuth(state) {
            return state.isAuth
        },
        getUserGid(state) {
            return state.userGid
        }
    },
    mutations: {
        setPosts(state, posts) {
            state.posts = posts;
        },
        setTitle(state, title) {
            state.title = title;
        },
        setBody(state, body) {
            state.body = body;
        },
        setLoading(state, bool) {
            state.isPostsLoading = bool
        },
        setPage(state, page) {
            state.page = page
        },
        setSelectedSort(state, selectedSort) {
            state.selectedSort = selectedSort
        },
        setTotalPages(state, totalPages) {
            state.totalPages = totalPages
        },
        setSearchQuery(state, searchQuery) {
            state.searchQuery = searchQuery
        },
        setFirstName(state, firstName) {
            state.firstName = firstName;
        },
        setLastName(state, lastName) {
            state.lastName = lastName;
        },
        setUserGid(state, userGid) {
            state.userGid = userGid;
        },
        setJWT(state, jwt) {
            state.JWT = jwt;
            if(jwt !== '' && jwt !== null) state.isAuth = true
        },
        removePost(state, postId) {
            state.posts = state.posts.filter(post => post.gid !== postId);
        },
        addPost(state, post) {
            state.posts = [...state.posts, post];
        },
    },
    actions: {
        async fetchPosts({state, commit}) {
            try {
                commit('setLoading', true)
                axios.defaults.headers.common['Authorization'] = `Bearer ${state.JWT}`;
                const response = await axios.get('http://localhost:5000/api/posts')
                commit('setPosts', response.data )
            } catch (e) {
                console.log(e)
            } finally {
                commit('setLoading', false)
            }
        },
        async loginUser({state, commit}) {
            try {
                commit('setLoading', true)
                const newUser = {
                    firstName: state.firstName,
                    lastName: state.lastName
                }
                const response = await axios.post(`http://localhost:5000/api/users`, newUser)
                commit('setJWT', response.data.jwtToken)
                commit('setUserGid', response.data.userGid)
            } catch (e) {
                console.log(e)
            } finally {
                commit('setLoading', false)
            }
        },
        async addPost({state, commit}) {
            try {
                commit('setLoading', true)
                const newPost = {
                    title: state.title,
                    body: state.body,
                    userGid: state.userGid
                }
                const response = await axios.post(`http://localhost:5000/api/posts`, newPost)
                commit('addPost', response.data)
            } catch (e) {
                console.log(e)
            } finally {
                commit('setLoading', false)
            }
        },
        async deletePost({ commit }, postId) {
            console.log(postId, 'id')
            try {
                commit('setLoading', true);
                const response = await axios.delete(`http://localhost:5000/api/posts?gid=${postId}`);
                console.log(response.data);
                commit('removePost', postId);
            } catch (e) {
                console.log(e);
            } finally {
                commit('setLoading', false);
            }
        },
        // async showMyPosts({ state, commit }) {
        //     try {
        //         commit('setLoading', true);
        //         const response = await axios(`http://localhost:5000/api/posts/user?userGid=${state.userGid}`);
        //         console.log(response.data);
        //         commit('', postId);
        //     } catch (e) {
        //         console.log(e);
        //     } finally {
        //         commit('setLoading', false);
        //     }
        // }
    },
    namespaced: true
}