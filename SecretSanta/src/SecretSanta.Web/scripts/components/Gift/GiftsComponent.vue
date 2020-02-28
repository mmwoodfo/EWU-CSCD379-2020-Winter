
    <template>
        <div>
            <button class=" button" @click='createGift'>Create new</button>
            <table class="table">
                <thead>
                    <tr>
                        <th>id</th>
                        <th>Title</th>
                        <th>Description</th>
                        <th>Url</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>

                    <tr v-for="gift in Gifts" :id="gift.id">
                        <td> {{gift.id}}</td>
                        <td>{{gift.title}}</td>
                        <td>{{gift.description}}</td>
                        <td>{{gift.Url}}</td>
                        <td>
                            <button class="button" @click='setGift(gift)'>Edit</button>
                            <button class="button" @click='deteGift(gift)'>Edit</button>
                            <!--<a asp-action="Edit" asp-route-id="@item.Id">Edit</a> |
    <a asp-action="Delete" asp-route-id="@item.Id">Delete</a>-->
                        </td>
                    </tr>

                </tbody>
            </table>
            <gift-details-component v-if="selectedGift != null" 
                                    :gift="selectedGift"
                                    @gift-saved="refreshGifts()"></gift-details-component>
        </div>
    </template>
<script lang="ts">
    import { Vue, Component } from 'vue-property-decorator';
    import { Gift, GiftClient } from '../../secretsanta-client';
    import GiftDetailsComponent from './GiftDetailsComponent.vue';
    @Component({
        components: {
            GiftDetailsComponent
        }
    })
    export default class GiftsComponent extends Vue {
        gifts: Gift[] = null;
        selectedGift: Gift = null;
        async loadGifts() {
            let giftClient = new GiftClient();
            this.gifts = await giftClient.getAll();
        }
        async mounted() {
            await this.loadGifts();
        }
        setGift(gift: Gift) {
            this.selectedGift = gift;
        }
        
        createGift() {
            this.selectedGift = <Gift>{};
        }
        async refreshGifts() {
            this.selectedGift = null;
           await this.loadGifts();
        }
        async deleteGift(gift: Gift) {
            let giftClient = new GiftClient();
            //need a listen box
            if (confirm('are you sure you want to delete ${gift.Title}')) {
                await giftClient.delete(gift.id);
            }
            await this.refreshGifts();
        }

    }
</script>


