input = [3, 1, 5, 1, 3, 1, 4, 1, 5]

def calculate_water(heights)
  left_index = 0
  right_index = heights.length - 1
  total = 0
  max_left_index = 0
  max_right_index = 0
  while left_index <= right_index
    if heights[left_index] <= heights[right_index]
      if heights[left_index] >= max_left_index
        max_left_index = heights[left_index]
      else
        total += max_left_index - heights[left_index]
      end
      left_index += 1
    else
      if heights[right_index] >= max_right_index
        max_right_index = heights[right_index]
      else
        total += max_right_index - heights[right_index]
      end
      right_index -= 1
    end
  end

  total
end

puts calculate_water(input)
