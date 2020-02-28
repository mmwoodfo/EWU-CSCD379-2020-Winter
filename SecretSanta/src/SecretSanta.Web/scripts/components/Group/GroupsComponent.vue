
<template>
    <div>
        <button class=" button" @click='createGroup'>Create new</button>
        <div class="field">
            <label class="label">Search</label>
            <div class="control">
                <input class="input" type="text" v-model="search" />
            </div>
        </div>
        <table class="table">
            <thead>
                <tr>
                    <th>id</th>
                    <th>Title</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>

                <tr v-for="group in groups" :id="group.id" v-if="group.title.includes(search)">
                    <td>{{group.id}}</td>
                    <td>{{group.title}}</td>
                    <td>
                        <button class="button" @click='setGroup(group)'>Edit</button>
                        <button class="button" @click='deleteGroup(group)'>Delete</button>
                    </td>
                </tr>

            </tbody>
        </table>
        <group-details-component v-if="selectedGroup != null"
                                 :group="selectedGroup"
                                 @group-saved="refreshGroups"></group-details-component>
    </div>
</template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { Group, GroupClient } from '../../secretsanta-client';
    import GroupDetailsComponent from './GroupDetailsComponent.vue';
    @Component({
        components: {
            GroupDetailsComponent
        }
    })
    export default class GroupsComponent extends Vue {
        groups: Group[] = null;
        selectedGroup: Group = null;
        search: string = "";
        async loadGroups() {
            let groupClient = new GroupClient();
            this.groups = await groupClient.getAll();
        }
        async mounted() {
            await this.loadGroups();
        }
        setGroup(group: Group) {
            this.selectedGroup = group;
        }
        createGroup() {
            this.selectedGroup = <Group>{};
        }
        async refreshGroups() {
            this.selectedGroup = null;
            await this.loadGroups();
        }
        async deleteGroup(group: Group) {
            let groupClient = new GroupClient();
            if (confirm(`are you sure you want to delete ${group.title}`)) {
                await groupClient.delete(group.id);
            }
            await this.refreshGroups();
        }

    }
</script>


