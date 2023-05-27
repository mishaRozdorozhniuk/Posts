import Main from "@/pages/LogIn";
import PostPage from "@/pages/PostPage";
import About from "@/pages/About";
import PostsPageWithStore from "@/pages/PostsPageWithStore";
import { createRouter, createWebHistory } from 'vue-router';
import { useStore } from 'vuex';
import LogIn from "@/pages/LogIn";

const router = createRouter({
    history: createWebHistory(),
    routes: [
        {
            path: '/',
            component: Main,
            meta: { requiresAuth: true },
        },
        {
            path: '/about',
            component: About,
            meta: { requiresAuth: true },
        },
        {
            path: '/posts/:id',
            component: PostPage,
            meta: { requiresAuth: true },
        },
        {
            path: '/posts',
            component: PostsPageWithStore,
            meta: { requiresAuth: true },
        },
        {
            path: '/login',
            component: LogIn,
        },
    ],
});

router.beforeEach((to, from, next) => {
    const store = useStore();
    const isAuthenticated = store.state.post.isAuth;
    console.log(
        isAuthenticated
    )

    if (to.matched.some((record) => record.meta.requiresAuth)) {
        if (!isAuthenticated) {
            next('/login');
        } else {
            next();
        }
    } else {
        next();
    }
});

export default router;
