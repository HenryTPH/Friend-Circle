class Solution {
    /**
    * @param number tomatoSlices
    * @param number CheeseSlices
    
    * @returns number[]
    */
        Burgers(tomatoSlices, CheeseSlices) {
            // code here
            var result = [];
            var cal = 4 * CheeseSlices - tomatoSlices;
            var x = 0;
            var y = 0;
            if(cal > 0 && cal % 2 == 0) {
                y = cal/2;
                x = CheeseSlices - y;
                if(x > 0) {
                    result.push(x);
                    result.push(y);
                }
            }
            return result;
        }
    }