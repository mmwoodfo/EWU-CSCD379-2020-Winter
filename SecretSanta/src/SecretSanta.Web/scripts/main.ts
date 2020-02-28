import '../styles/site.scss';

import GiftsComponent from './components/Gift/GiftsComponent.vue'
import UsersComponent from './components/User/UsersComponent.vue'
import GroupsComponent from './components/Group/GroupsComponent.vue'

import Vue from 'vue';
document.addEventListener("DOMContentLoaded", async () => {
    if (document.getElementById('giftList'))
        new Vue({
            render: h => h(GiftsComponent)
        }).$mount('#giftList');
    if (document.getElementById('userList'))
        new Vue({
            render: h => h(UsersComponent)
        }).$mount('#userList');
    if (document.getElementById('groupList'))
        new Vue({
            render: h => h(GroupsComponent)
        }).$mount('#groupList');
});