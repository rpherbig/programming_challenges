class MazeSolver
  def initialize(filename)
    unless filename
      puts 'Must provide a file name'
      return
    end
    @maze = File.readlines(filename).map(&:chomp).map(&:chars)
    @start = find('S')
    @end = find('E')
    @best_path = []

    check(*@start, [])
    # @best_path.each_with_index { |cell, idx| @maze[cell.first][cell.last] = idx }
    puts "#{@best_path.empty? ? 'No solution found' : @best_path}"
  end

  def find(char)
    @maze.each_with_index do |row, row_idx|
      row.each_with_index do |cell, col_idx|
        return [row_idx, col_idx] if cell == char
      end
    end
  end

  def next_moves(row, col)
    [[row + 1, col],
     [row , col + 1],
     [row - 1, col],
     [row, col - 1]]
  end

  def check(row, col, current_path)
    return if current_path.include?([row, col])
    return if @maze[row][col] == '*'

    current_path << [row, col]
    return if !@best_path.empty? && @best_path.length < current_path.length

    if [row, col] == @end
      @best_path = current_path
      return 
    end

    next_moves(row, col).each { |new_row, new_col| check(new_row, new_col, current_path.dup) }
  end
end

MazeSolver.new(ARGV[0])
