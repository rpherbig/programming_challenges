# Copyright: Robert P Herbig, 2011

# Interpreter at http://ideone.com/

rows <- 3
cols <- 6
start <- matrix( c(0,1,2,3,4,5, 6,7,8,9,10,11, 12,13,14,15,16,17), nrow=rows, ncol=cols, byrow=TRUE )
print(start)
end <- start
for(i in 1:rows) {
  for(j in 1:cols) {
    if(start[i,j]==0) {
      end[i,] <- 0
      end[,j] <- 0
    }
  }
}
print(end)

