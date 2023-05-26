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
        lastName: ''
    }),
    getters: {
        sortedPosts(state) {
            return [...state.posts].sort((post1, post2) =>  post1[state.selectedSort]?.localeCompare(post2[state .selectedSort]))
        },
        sortedAndSearchedPosts(state, getters) {
            return getters.sortedPosts.filter(post => post.title.toLowerCase().includes(state.searchQuery.toLowerCase()))
        }
    },
    mutations: {
        setPosts(state, posts) {
            state.posts = posts;
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
        }
    },
    actions: {
        async fetchPosts({state, commit}) {
            try {
                commit('setLoading', true)
                const response = await axios('https://jsonplaceholder.typicode.com/posts', {
                    params: {
                        _page: state.page,
                        _limit: state.limit
                    }
                })
                commit('setTotalPages', Math.ceil(response.headers['x-total-count'] / state.limit))
                commit('setPosts', response.data )
            } catch (e) {
                console.log(e)
            } finally {
                commit('setLoading', false)
            }
        },
        submitForm({ commit, state }) {
            commit('setFirstName', state.firstName);
            commit('setLastName', state.lastName);


            commit('setFirstName', '');
            commit('setLastName', '');
        }
    },
    namespaced: true
}